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
}