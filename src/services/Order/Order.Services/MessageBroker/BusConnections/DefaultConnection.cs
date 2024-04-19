using System.Net.Sockets;
using Order.Services.MessageBroker.Interfaces;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Client.Infrastructure.BusConnections;

public class DefaultConnection : IMessageBrokerPersistentConnection
{
    private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private readonly int _retryCount;
        private bool _disposed;

        public DefaultConnection(IConnectionFactory connectionFactory,
                                                   int retryCount)
        {
            _connectionFactory = connectionFactory;
            _retryCount = retryCount;
        }

        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.IsOpen && !_disposed;
            }
        }
        public bool TryConnect()
        {
            var policy = RetryPolicy.Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                  {
                      //logging
                  });
            policy.Execute(() =>
            {
                _connection = _connectionFactory.CreateConnection();
            });

            if (IsConnected)
            {
                _connection.ConnectionShutdown += OnConnectionShutdown;
                _connection.CallbackException += OnCallbackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;

                return true;
            }
            else
            {
                //logging
                return false;
            }
        }

        private void OnConnectionBlocked(object? sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;
            //logging
            TryConnect();
        }

        private void OnCallbackException(object? sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;
            //logging
            TryConnect();
        }

        private void OnConnectionShutdown(object? sender, ShutdownEventArgs reason)
        {
            if (_disposed) return;
            //logging
            TryConnect();
        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                //logging
                throw new InvalidOperationException("No RabbitMQ connections are available to perfrom this action.");
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (Exception ex)
            {
                //logging
            }
        }
}
