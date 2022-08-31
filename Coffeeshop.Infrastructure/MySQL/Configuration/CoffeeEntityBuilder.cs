using Coffeeshop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coffeeshop.Infrastructure.MySQL.Configuration;

internal class CoffeeEntityTypeConfiguration : IEntityTypeConfiguration<Coffee>
{
    public void Configure(EntityTypeBuilder<Coffee> builder)
    {
        builder.ToTable("coffee");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

        /// We seed the datbase with data
        builder.HasData(
            new Coffee { Id = 1, Name = "Caffé Americano", Type = "Americanos", Quantity = 10 },
            new Coffee { Id = 2, Name = "Veranda Blend", Type = "Brewed Coffees", Quantity = 10 },
            new Coffee { Id = 3, Name = "Caffé Misto", Type = "Brewed Coffees", Quantity = 10 },
            new Coffee { Id = 4, Name = "Cappuccino", Type = "Cappuccinos", Quantity = 10 },
            new Coffee { Id = 5, Name = "Espresso", Type = "Espresso", Quantity = 10 },
            new Coffee { Id = 6, Name = "Flat White", Type = "Flat White", Quantity = 10 },
            new Coffee { Id = 7, Name = "Honey Almond milk", Type = "Espresso", Quantity = 10 },
            new Coffee { Id = 8, Name = "Pumpkin Spice Latte", Type = "Lattes", Quantity = 10 },
            new Coffee { Id = 9, Name = "Caffé Latte", Type = "Lattes", Quantity = 10 },
            new Coffee { Id = 10, Name = "Cinnamon Dolce Latte", Type = "Lattes", Quantity = 10 },
            new Coffee { Id = 11, Name = "Apple Crisp Oatmilk", Type = "Macchiatos", Quantity = 10 },
            new Coffee { Id = 12, Name = "Caramel Macchiato", Type = "Macchiatos", Quantity = 10 },
            new Coffee { Id = 13, Name = "Espresso Macchiato", Type = "Macchiatos", Quantity = 10 },
            new Coffee { Id = 14, Name = "Caffé Mocha", Type = "Mochas", Quantity = 10 },
            new Coffee { Id = 15, Name = "White Chocolate Mocha", Type = "Mochas", Quantity = 10 }
        );
    }
}
