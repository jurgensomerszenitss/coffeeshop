using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Infrastructure.MySQL;
using Coffeeshop.Infrastructure.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coffeeshop.Infrastructure;

public static class Bootstrapper
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddTransient<IUnitOfWork, UnitOfWork>()            
            .AddMySql(configuration)
            .AddRedis(configuration);
    }

    private static IServiceCollection AddMySql(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CoffeeContext");
#if DEBUG
        services.AddDbContext<CoffeeContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
#else
        services.AddDbContext<CoffeeContext>(options => options.UseInMemoryDatabase("CoffeeDb"));
#endif
        return services;
    }

    private static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RedisOptions>(options => configuration.GetSection("Redis").Bind(options));

        services.AddSingleton<IShopCache, ShopCache>();
        services.AddSingleton<IProcessQueue, ProcessQueue>();

        services.AddTransient<RedisContext>();

        return services;
    }
}