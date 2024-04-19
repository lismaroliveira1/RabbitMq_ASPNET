using RabbitMQ.Client;

namespace Order.Services.MessageBroker.Interfaces;

public interface IMessageBrokerPersistentConnection : IDisposable
{
    bool IsConnected { get; }
    bool TryConnect();
    IModel CreateModel();
}