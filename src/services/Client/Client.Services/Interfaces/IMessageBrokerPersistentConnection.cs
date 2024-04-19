using RabbitMQ.Client;

namespace Client.Services.Interfaces;

public interface IMessageBrokerPersistentConnection : IDisposable
{
    bool IsConnected { get; }
    bool TryConnect();
    IModel CreateModel();
}