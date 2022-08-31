using Coffeeshop.Domain.Commands;
using Coffeeshop.Domain.Models;

namespace Coffeeshop.Domain.Tests.Commands;

internal class OrderCreateTests : TestBase
{
    [Test]
    public void Map_Command_To_Dto()
    {
        // arrange
        var item = Fixture.Create<OrderCreate.Command>();

        // act
        var actual = item.Adapt<Order>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.Name, Is.EqualTo(item.Name));
        Assert.That(actual.Items.Count, Is.EqualTo(item.Items.Count));
    }

    [Test]
    public void Map_CommandItem_To_DtoItem()
    {
        // arrange
        var item = Fixture.Create<OrderCreate.CommandItem>();

        // act
        var actual = item.Adapt<OrderItem>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.CoffeeId, Is.EqualTo(item.CoffeeId));
        Assert.That(actual.Quantity, Is.EqualTo(item.Quantity));
    }
}
