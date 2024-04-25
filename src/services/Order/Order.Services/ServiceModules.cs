using MessageBroker.EventBus;
using MessageBroker.EventBus.Producer;
using Microsoft.Extensions.DependencyInjection;
using Order.Services.Bundles;
using Order.Services.Interfaces;
using Order.Services.Profiles;
using RabbitMQ.Client;


namespace Order.Services;
public static class ServiceModules {
    public static IServiceCollection AddServiceModules(this IServiceCollection services) {
        services.AddAutoMappersServices();
        services.AddServiceScoped();
        services.AddMessageBusServicesConfig();
        return services;
    }
    private static IServiceCollection AddAutoMappersServices(this IServiceCollection services) {
        services.AddAutoMapper(typeof(OrderProfile));
        return services;
    }
    private static IServiceCollection AddServiceScoped(this IServiceCollection services) {
        services.AddScoped<IOrderService, OrderService>();
        return services;
    }

    private static IServiceCollection AddMessageBusServicesConfig(this IServiceCollection services)
    {
        services.AddScoped<ProducerEvent>();
        services.AddSingleton<MbClient>();
        return services;
    }
}