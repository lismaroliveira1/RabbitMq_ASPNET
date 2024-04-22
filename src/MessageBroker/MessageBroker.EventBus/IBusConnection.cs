using RabbitMQ.Client;

namespace MessageBroker.EventBus
{
    public interface IBusConnection : IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();
    }
}
