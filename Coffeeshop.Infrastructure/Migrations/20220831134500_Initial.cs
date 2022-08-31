using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coffeeshop.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "coffee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantity = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coffee", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "order_item",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    CoffeeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_item_coffee_CoffeeId",
                        column: x => x.CoffeeId,
                        principalTable: "coffee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_order_item_order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "coffee",
                columns: new[] { "Id", "Name", "Quantity", "Type" },
                values: new object[,]
                {
                    { 1L, "Caffé Americano", 10L, "Americanos" },
                    { 2L, "Veranda Blend", 10L, "Brewed Coffees" },
                    { 3L, "Caffé Misto", 10L, "Brewed Coffees" },
                    { 4L, "Cappuccino", 10L, "Cappuccinos" },
                    { 5L, "Espresso", 10L, "Espresso" },
                    { 6L, "Flat White", 10L, "Flat White" },
                    { 7L, "Honey Almond milk", 10L, "Espresso" },
                    { 8L, "Pumpkin Spice Latte", 10L, "Lattes" },
                    { 9L, "Caffé Latte", 10L, "Lattes" },
                    { 10L, "Cinnamon Dolce Latte", 10L, "Lattes" },
                    { 11L, "Apple Crisp Oatmilk", 10L, "Macchiatos" },
                    { 12L, "Caramel Macchiato", 10L, "Macchiatos" },
                    { 13L, "Espresso Macchiato", 10L, "Macchiatos" },
                    { 14L, "Caffé Mocha", 10L, "Mochas" },
                    { 15L, "White Chocolate Mocha", 10L, "Mochas" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_item_CoffeeId",
                table: "order_item",
                column: "CoffeeId");

            migrationBuilder.CreateIndex(
                name: "IX_order_item_OrderId",
                table: "order_item",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_item");

            migrationBuilder.DropTable(
                name: "coffee");

            migrationBuilder.DropTable(
                name: "order");
        }
    }
}
