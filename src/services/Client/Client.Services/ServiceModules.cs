using Client.Services.Interfaces;
using Client.Services.Messaging;
using Client.Services.Profiles;
using Client.Services.Services;
using MessageBroker.EventBus;
using MessageBroker.EventBus.Producer;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Client.Services;
public static class ServiceModules
{
    public static IServiceCollection AddServiceModules(this IServiceCollection services)
    {
        services.AddAutoMappersServices();
        services.AddServiceScoped();
        services.AddMessageBusServicesConfig();
        return services;
    }
    private static IServiceCollection AddAutoMappersServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(PersonAutoMapProfile));
        return services;
    }
    private static IServiceCollection AddServiceScoped(this IServiceCollection services)
    {
        services.AddScoped<IPersonService, PersonService>();
        return services;
    }
    private static IServiceCollection AddMessageBusServicesConfig(this IServiceCollection services)
    {
        services.AddSingleton<IBusConnection>(sp =>
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

            return new BusConnection(factory, retryCount);
        });
        services.AddScoped<ProducerEvent>();
        services.AddSingleton<RpcServer>();
        return services;
    }
}