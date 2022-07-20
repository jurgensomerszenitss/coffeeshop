using Coffeeshop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coffeeshop.Infrastructure.MySQL.Configuration;

internal class CoffeeEntityTypeConfiguration : IEntityTypeConfiguration<Coffee>
{
    public void Configure(EntityTypeBuilder<Coffee> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

        builder.HasOne(p => p.Supplier).WithMany().HasForeignKey(p => p.SupplierId);
    }
}
