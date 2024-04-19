
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Order.Infra.Contexts;
using Order.Infra.Interfaces;
using Order.Infra.Repositories;

namespace Order.Infra;
public static class InfraModules
{

    public static IServiceCollection AddInfraModules(this IServiceCollection services)
    {
        services.AddAdbContext();
        services.AddServiceScoped();
        return services;
    }
    private static IServiceCollection AddAdbContext(this IServiceCollection services)
    {
        var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=SampleDbDriver; PORT=5433";
        services.AddDbContext<OrderContext>(options => options.UseNpgsql(connectionString));
        return services;
    }

    private static IServiceCollection AddServiceScoped(this IServiceCollection services) {
        services.AddScoped<IOrderRepository, OrderRepository>();
        return services;
    }
}