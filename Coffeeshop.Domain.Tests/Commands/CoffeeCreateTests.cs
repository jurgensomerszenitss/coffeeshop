using Coffeeshop.Domain.Commands;
using Coffeeshop.Domain.Models;

namespace Coffeeshop.Domain.Tests.Commands;

internal class CoffeeCreateTests : TestBase
{
    [Test]
    public void Map_Command_To_Dto()
    {
        // arrange
        var item = Fixture.Create<CoffeeCreate.Command>();

        // act
        var actual = item.Adapt<Coffee>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.Name, Is.EqualTo(item.Name));
        Assert.That(actual.Type, Is.EqualTo(item.Type));
    }
}
