using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Coffeeshop.Infrastructure.MySQL;

internal class CoffeeContextFactory : IDesignTimeDbContextFactory<CoffeeContext>
{
    public CoffeeContext CreateDbContext(string[] args)
    {
        var optionsbuilder = new DbContextOptionsBuilder<CoffeeContext>();
        var connectionString = "server=localhost;user=coffee_user;password=Coffee123;database=CoffeeDb";
        optionsbuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        return new CoffeeContext(optionsbuilder.Options);
    }
}
