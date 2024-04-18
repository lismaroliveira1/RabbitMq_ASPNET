using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using ServerLogger.Infra.Connections;
using ServerLogger.Infra.Interfaces;
using ServerLogger.Infra.Producer;
using ServerLogger.Infra.Server;

namespace ServerLogger.Infra.Applications;

public static class MbServiceApplication {

    public static IServiceCollection AddMessageBrokerService(IServiceCollection services)
    {
        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            factory.AutomaticRecoveryEnabled = true;
            factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(5);
            factory.TopologyRecoveryEnabled = true;
    
            if (!string.IsNullOrWhiteSpace("guest"))
                factory.UserName = "guest";
    
            if (!string.IsNullOrWhiteSpace("guest"))
                factory.Password = "guest";
    
            var retryCount = 3;
    
            if (!string.IsNullOrWhiteSpace("3"))
                retryCount = int.Parse("3");
    
            return new DefaultRabbitMQPersistentConnection(factory, retryCount);
        });
        services.AddScoped<EventBusRabbitMQProducer>();
        services.AddSingleton<RpcServer>();
        return services;
    }
}