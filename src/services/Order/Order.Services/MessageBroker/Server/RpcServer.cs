using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Order.Domain.Entities;
using Order.Infra.Contexts;
using Order.Services.MessageBroker.Core;
using Order.Services.MessageBroker.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Order.Services.MessageBroker.Server
{
    public class RpcServer
    {
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly IDbContextFactory<OrderContext> myDbContextFactory;
        public RpcServer(IRabbitMQPersistentConnection persistentConnection, IDbContextFactory<OrderContext> dbContextFactory)
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
            List<OrderEntity> partners = new List<OrderEntity>();
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
                            partners = await dbContext.Orders.AsNoTracking().Where(x => x.Id == userId).ToListAsync();
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