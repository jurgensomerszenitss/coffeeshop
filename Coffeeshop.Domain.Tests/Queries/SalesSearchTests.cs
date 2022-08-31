using Coffeeshop.Domain.Models;
using Coffeeshop.Domain.Queries;

namespace Coffeeshop.Domain.Tests.Queries;

internal class SalesSearchTests : TestBase
{
    [Test]
    public void Map_Model_To_Response()
    {
        // arrange
        var item = Fixture.Create<Coffee>();

        // act
        var actual = item.Adapt<SalesSearch.Response>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.CoffeeId, Is.EqualTo(item.Id));
        Assert.That(actual.CoffeeName, Is.EqualTo(item.Name));
        Assert.That(actual.Quantity, Is.EqualTo(item.Orders.Sum(o => o.Quantity)));
    }
}
