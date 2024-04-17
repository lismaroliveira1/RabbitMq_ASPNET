using Client.Services.Interfaces;
using Client.Services.Profiles;
using Microsoft.Extensions.DependencyInjection;
using Client.Service.Services;


namespace Client.Service.Services;
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
}