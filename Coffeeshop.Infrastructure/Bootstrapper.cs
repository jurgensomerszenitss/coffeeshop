using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Infrastructure.MySQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coffeeshop.Infrastructure;

public static class Bootstrapper
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        var connectionString = configuration.GetConnectionString("CoffeeContext");
#if DEBUG
        services.AddDbContext<CoffeeContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
#else
        services.AddDbContext<CoffeeContext>(options => options.UseInMemoryDatabase("CoffeeDb"));
#endif

        return services;
    }
}