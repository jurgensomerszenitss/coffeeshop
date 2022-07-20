using Coffeeshop.Domain.Commands;
using Coffeeshop.Domain.Models;

namespace Coffeeshop.Domain.Tests.Commands;

internal class CoffeeUpdateTests : TestBase
{
    [Test]
    public void Map_Command_To_Dto()
    {
        // arrange
        var item = Fixture.Create<CoffeeUpdate.Command>();

        // act
        var actual = item.Adapt<Coffee>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.Id, Is.EqualTo(item.Id));
        Assert.That(actual.Name, Is.EqualTo(item.Name));
        Assert.That(actual.Type, Is.EqualTo(item.Type));
        Assert.That(actual.SupplierId, Is.EqualTo(item.SupplierId));
    }
}
