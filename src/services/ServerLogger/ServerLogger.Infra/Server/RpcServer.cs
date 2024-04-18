using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServerLogger.Infra.Core;
using ServerLogger.Infra.Interfaces;
using ServerLogger.Infra.Models;
using System.Text;

namespace ServerLogger.Infra.Server
{
    public class RpcServer
    {
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        public RpcServer(IRabbitMQPersistentConnection persistentConnection)
        {
            _persistentConnection = persistentConnection;
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
        private void ReceivedEventAsync(object sender, BasicDeliverEventArgs ea, IModel channel)
        {

            string response = string.Empty;
            var props = ea.BasicProperties;
            var replyProps = channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;
            try
            {
                var message = Encoding.UTF8.GetString(ea.Body.Span);
                var data = JsonConvert.DeserializeObject<Request>(message);
                if (ea.RoutingKey == $"{EventBusConstants.RdcPublishQueue}")
                {
                    Console.WriteLine("");
                }
                response = JsonConvert.SerializeObject(new Response() { Success = true, Message = "Message received from server", Payload = null! });
            }
            catch (Exception ex)
            {
                //logging
                response = JsonConvert.SerializeObject(new Response() { Success = false, Message = "Failure" });
                throw new Exception();

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