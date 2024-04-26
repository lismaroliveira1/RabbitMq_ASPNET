using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using MessageBroker.Core.Model;
using MessageBroker.EventBus;
using MessageBroker.EventBus.Core;
using Client.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Client.Infrastructure.Context;

namespace Client.Services.Messaging
{
    public class RpcServer
    {
        private readonly IBusConnection _persistentConnection;
        private readonly IDbContextFactory<PersonContext> _context;
        public RpcServer(IBusConnection persistentConnection, IServiceScopeFactory factory, IDbContextFactory<PersonContext> context)
        {
            _persistentConnection = persistentConnection;
            _context = context;
        }
        public void Consume(string queue)
        {
            if (!_persistentConnection.IsConnected)
                _persistentConnection.TryConnect();

            var channel = _persistentConnection.CreateModel();

            var args = new Dictionary<string, object>();
            args.Add("x-single-active-consumer", true);

            channel.QueueDeclare(
                   queue: queue,
                   durable: true,
                   exclusive: false,
                   autoDelete: false,
                   arguments: args);
            channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
            consumer.Received += (model, ea) =>
            {
                ReceivedEventAsync(model, ea, channel);
            };
        }
        private async Task ReceivedEventAsync(object sender, BasicDeliverEventArgs ea, IModel channel)
        {
            string response = null;

            var props = ea.BasicProperties;
            var replyProps = channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;

            try
            {
                var message = Encoding.UTF8.GetString(ea.Body.Span);
                var data = JsonConvert.DeserializeObject<Request>(message);
                List<PersonEntity> partners = new List<PersonEntity>();
                if (ea.RoutingKey == $"{EventBusConstants.RdcPublishQueue}")
                {
                    string method = data!.Method;
                    switch (method)
                    {
                        case "GetUser":
                            int userId = data.Payload["Id"];
                            var dbContext = _context.CreateDbContext();
                            partners = await dbContext.Person.AsNoTracking().Where(x => x.Id == userId).ToListAsync();
                            break;
                    }
                }
                response = JsonConvert.SerializeObject(new Response() { Success = true, Payload = partners.FirstOrDefault()});
            }
            catch (Exception ex)
            {
                //logging
                response = JsonConvert.SerializeObject(new Response() { Success = false, Payload = "Failure" });
            }
            finally
            {
                var responseBytes = Encoding.UTF8.GetBytes(response);
                channel.BasicPublish(
                    exchange: "",
                    routingKey: props.ReplyTo,
                    basicProperties: replyProps,
                    body: responseBytes);
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            }
        }
        public void Disconnect()
        {
            _persistentConnection.Dispose();
        }
    }
}