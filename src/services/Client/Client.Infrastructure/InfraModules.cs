
using Client.Infrastructure.Context;
using Client.Infrastructure.Interfaces;
using Client.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Motorcycle.Infrastructure;

public static class InfraModules
{
    internal static IConfiguration _configuration;

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
        services.AddDbContext<PersonContext>(options => options.UseNpgsql(connectionString));
        return services;
    }

    

}