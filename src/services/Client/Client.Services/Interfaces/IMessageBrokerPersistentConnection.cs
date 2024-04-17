using RabbitMQ.Client;

namespace Client.Infrastructure.Interfaces;

public interface IMessageBrokerPersistentConnection : IDisposable
{
    bool IsConnected { get; }
    bool TryConnect();
    IModel CreateModel();
}