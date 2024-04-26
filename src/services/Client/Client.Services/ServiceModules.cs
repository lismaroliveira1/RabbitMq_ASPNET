using Client.Infrastructure.Messaging;
using Client.Services.Interfaces;
using Client.Services.Profiles;
using Client.Services.Services;
using MessageBroker.EventBus.Producer;
using Microsoft.Extensions.DependencyInjection;

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
        services.AddScoped<ILoggerService, LoggerService>();
        return services;
    }
    private static IServiceCollection AddMessageBusServicesConfig(this IServiceCollection services)
    {
        
        services.AddScoped<ProducerEvent>();
        services.AddSingleton<RpcServer>();
        return services;
    }
}