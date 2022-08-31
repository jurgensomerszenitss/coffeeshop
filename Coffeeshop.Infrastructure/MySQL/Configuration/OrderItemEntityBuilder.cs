using Coffeeshop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coffeeshop.Infrastructure.MySQL.Configuration;

internal class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_item");
        
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Order).WithMany(p => p.Items).HasForeignKey(p => p.OrderId);
        builder.HasOne(p => p.Coffee).WithMany(p => p.Orders).HasForeignKey(p => p.CoffeeId).OnDelete(DeleteBehavior.Restrict);
    }
}
