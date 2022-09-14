using Coffeeshop.Api.Dto;
using Coffeeshop.Domain.Models;

namespace Coffeeshop.Api.Tests.Maps;

internal class ShopMapTests : TestBase
{
    

    [Test]
    public void Map_Status_To_GetDto()
    {
        // arrange
        var item = Fixture.Create<ShopStatus>();

        // act
        var actual = item.Adapt<ShopGetStatusDto>();

        // assert
        Assert.IsNotNull(actual);       
        Assert.That(actual.Status, Is.EqualTo(item.ToString()));
    }     
}
