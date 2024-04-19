using RabbitMQ.Client;

namespace Client.Services.MessageBroker
{
    public interface IRabbitMqPersistentConnection : IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();
    }
}
