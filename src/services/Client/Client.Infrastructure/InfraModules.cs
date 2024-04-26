using Client.Infrastructure.Context;
using Client.Infrastructure.Interfaces;
using Client.Infrastructure.Messaging;
using Client.Infrastructure.Repositories;
using MessageBroker.EventBus.Producer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Infrastructure;

public static class InfraModules
{
    public static IServiceCollection AddInfraModules(this IServiceCollection services)
    {
        services.AddMessageBus();
        services.AddAdbContext();
        return services;
    }
    private static IServiceCollection AddMessageBus(this IServiceCollection services)
    {
        services.AddScoped<IPersonRepository, PersonRepository>();
        return services;
    }
    private static IServiceCollection AddAdbContext(this IServiceCollection services)
    {
        var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=SampleDbDriver; PORT=5433";
        services.AddDbContextFactory<PersonContext>(options => options.UseNpgsql(connectionString));
        return services;
    }

    private static IServiceCollection AddMessageBusServicesConfig(this IServiceCollection services)
    {
        services.AddScoped<ProducerEvent>();
        services.AddSingleton<RpcServer>();
        return services;
    }

}