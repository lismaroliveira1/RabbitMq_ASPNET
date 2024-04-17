using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using User.Infrastructure.Messages;
using Client.Service.MessageBroker;
using Client.Service.MessageBroker.Core;
using Client.Service.MessageBroker.Model;
using Client.Infrastructure.Context;
using Client.Domain.Entities;

namespace RabbitMQ.Server.Messaging
{
    public class RpcServer
    {
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly IDbContextFactory<PersonContext> myDbContextFactory;
        public RpcServer(IRabbitMQPersistentConnection persistentConnection, IDbContextFactory<PersonContexta> dbContextFactory)
        {
            _persistentConnection = persistentConnection;
            myDbContextFactory = dbContextFactory;
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
        private async void ReceivedEventAsync(object sender, BasicDeliverEventArgs ea, IModel channel)
        {

            string response = string.Empty;
            var props = ea.BasicProperties;
            var replyProps = channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;
            List<Person> partners = new List<Person>();
            try
            {
                var message = Encoding.UTF8.GetString(ea.Body.Span);
                var data = JsonConvert.DeserializeObject<Request>(message);
                if (ea.RoutingKey == $"{EventBusConstants.RdcPublishQueue}")
                {
                    string method = data!.Method;
                    switch (method)
                    {
                        case "GetUser":
                            var userId = int.Parse(data.Payload["Id"]);
                            var dbContext = myDbContextFactory.CreateDbContext();
                            partners = await dbContext.Person.AsNoTracking().Where(x => x.Id == userId).ToListAsync();
                            var user = partners.FirstOrDefault()!;
                            break;
                    }
                }
                response = JsonConvert.SerializeObject(new Response() { Success = true, Message = "Message received from server", Payload = partners.FirstOrDefault()! });
            }
            catch (Exception ex)
            {
                //logging
                response = JsonConvert.SerializeObject(new Response() { Success = false, Message = "Failure" });
            }
            finally
            {
                var responseBytes = Encoding.UTF8.GetBytes(response);
                if (props.ReplyTo != null && !channel.IsClosed)
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