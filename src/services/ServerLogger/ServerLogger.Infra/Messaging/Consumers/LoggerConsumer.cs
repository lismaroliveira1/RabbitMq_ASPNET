using System.Text;
using MessageBroker.EventBus;
using MessageBroker.EventBus.Core;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServerLogger.Infra.Interfaces;

namespace ServerLogger.Infra.Messaging.Consumers
{
    public class Consumer
    {
        private readonly IBusConnection _persistentConnection;
        private readonly ILoggerFactory _log;

        public Consumer(IBusConnection persistentConnection, ILoggerFactory log)
        {
            _persistentConnection = persistentConnection;
            _log = log;
        }

        public void Consume(string queue)
        {

            if (!_persistentConnection.IsConnected)
                _persistentConnection.TryConnect();

            var channel = _persistentConnection.CreateModel();
            var args = new Dictionary<string, object>();
            args.Add("x-single-active-consumer", true);

            channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments : args);

            // BasicQos method uses which to make it possible to limit the number of unacknowledged messages on a channel.
            channel.BasicQos(0, 1, true);
            var consumer = new EventingBasicConsumer(channel);

            BasicGetResult result = channel.BasicGet(queue, false);
            channel.BasicRecover(true);
            consumer.Received += (ch, ea) =>
            {
                ReceivedEvent(ch, ea, channel);
            };

            consumer.Shutdown += (o, e) =>
            {
                //logging
            };

            channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
        }

        private void ReceivedEvent(object sender, BasicDeliverEventArgs e, IModel channel)
        {
            string customRetryHeaderName = "number-of-retries";
            int retryCount = HelperFunctions.GetRetryCount(e.BasicProperties, customRetryHeaderName);
            var message = Encoding.UTF8.GetString(e.Body.Span);

            try
            {
                var data = JsonConvert.DeserializeObject<dynamic>(message);
                var response = new HttpResponseMessage();

                if (e.RoutingKey == $"{EventBusConstants.DirectQueue}")
                {
                    string logMessage = data!["message"];
                    switch(data["type"]) {
                        case "Error":
                            _log.LogError(logMessage);
                        break;
                        case "Info":
                            _log.LogInfo(logMessage);
                        break;
                        case "Fatal":
                            _log.LogFatal(logMessage);
                        break;
                        case "Debug":
                            _log.LogDebug(logMessage);
                        break;
                        case "Warning":
                            _log.LogWarning(logMessage);
                        break;
                    }
                    
                
                }
            }
            catch (Exception ex)
            {
                if (retryCount != 3)
                {
                    IBasicProperties propertiesForCopy = channel.CreateBasicProperties();
                    IDictionary<string, object> headersCopy = HelperFunctions.CopyHeaders(e.BasicProperties);
                    propertiesForCopy.Headers = headersCopy;
                    propertiesForCopy.Headers[customRetryHeaderName] = ++retryCount;
                    channel.BasicPublish(e.Exchange, e.RoutingKey, propertiesForCopy, e.Body);
                }
                else
                {
                    // logging
                }
            }
            finally
            {
                channel.BasicAck(e.DeliveryTag, false);
            }
        }

        public void Disconnect()
        {
            _persistentConnection.Dispose();
        }
    }
}