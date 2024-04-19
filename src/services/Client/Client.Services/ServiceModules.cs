using Client.Services.Interfaces;
using Client.Services.MessageBroker.BusConnections;
using Client.Services.MessageBroker.Producer;
using Client.Services.Profiles;
using Client.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Client.Services;
public static class ServiceModules {
    public static IServiceCollection AddServiceModules(this IServiceCollection services) {
        services.AddAutoMappersServices();
        services.AddServiceScoped();
        return services;
    }
    private static IServiceCollection AddAutoMappersServices(this IServiceCollection services) {
        services.AddAutoMapper(typeof(PersonAutoMapProfile));
        return services;
    }
    private static IServiceCollection AddServiceScoped(this IServiceCollection services) {
        services.AddScoped<IPersonService, PersonService>();
        return services;
    }

    private static IServiceCollection AddMessageBusService(this IServiceCollection services)
    {

        services.AddSingleton<IMessageBrokerPersistentConnection>(sp =>
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            var retryCount = 3;
            return new DefaultConnection(factory, retryCount);
        });
        services.AddScoped<EventBusRabbitMQProducer>();
        services.AddSingleton<RpcClient>();
        return services;
    }
}