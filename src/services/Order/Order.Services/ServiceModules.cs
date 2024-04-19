using Microsoft.Extensions.DependencyInjection;
using Order.Services.Bundles;
using Order.Services.Interfaces;
using Order.Services.MessageBroker;
using Order.Services.MessageBroker.BusConnections;
using Order.Services.MessageBroker.Interfaces;
using Order.Services.MessageBroker.Producer;
using Order.Services.Profiles;
using RabbitMQ.Client;

namespace Order.Services;
public static class ServiceModules {
    public static IServiceCollection AddServiceModules(this IServiceCollection services) {
        services.AddAutoMappersServices();
        services.AddServiceScoped();
        services.AddMessageBusService();
        return services;
    }
    private static IServiceCollection AddAutoMappersServices(this IServiceCollection services) {
        services.AddAutoMapper(typeof(OrderProfile));
        return services;
    }
    private static IServiceCollection AddServiceScoped(this IServiceCollection services) {
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IRabbitMQPersistentConnection, DefaultRabbitMQPersistentConnection>();
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