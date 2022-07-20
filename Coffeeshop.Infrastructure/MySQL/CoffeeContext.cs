using Coffeeshop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Coffeeshop.Infrastructure.MySQL;

internal class CoffeeContext : DbContext
{
    public CoffeeContext(DbContextOptions<CoffeeContext> options) : base(options)
    {

    }

    public DbSet<Coffee>? Coffee { get; set; }
    public DbSet<Supplier>? Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    } 
}
