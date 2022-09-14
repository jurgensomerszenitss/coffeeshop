using Coffeeshop.Api.Dto;
using Coffeeshop.Domain.Commands;
using Coffeeshop.Domain.Models;
using Coffeeshop.Domain.Queries;

namespace Coffeeshop.Api.Tests.Maps;

internal class OrderMapTests : TestBase
{
    [Test]
    public void Map_QueryDto_To_Query()
    {
        // arrange
        var item = Fixture.Create<OrderQueryDto>();

        // act
        var actual = item.Adapt<OrderSearch.Query>();

        // assert
        Assert.IsNotNull(actual); 
        Assert.That(actual.Name, Is.EqualTo(item.Name)); 
    }

    [Test]
    public void Map_Model_To_SearchDto()
    {
        // arrange
        var item = Fixture.Create<Order>();

        // act
        var actual = item.Adapt<OrderSearchDto>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.Id, Is.EqualTo(item.Id));
        Assert.That(actual.Name, Is.EqualTo(item.Name));
        Assert.That(actual.Date, Is.EqualTo(item.Date));
        Assert.That(actual.Status, Is.EqualTo(item.Status.ToString()));
    }


    [Test]
    public void Map_Order_To_GetDto()
    {
        // arrange
        var item = Fixture.Create<Order>();

        // act
        var actual = item.Adapt<OrderGetDto>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.Id, Is.EqualTo(item.Id));
        Assert.That(actual.Name, Is.EqualTo(item.Name));
        Assert.That(actual.Date, Is.EqualTo(item.Date));
        Assert.That(actual.Status, Is.EqualTo(item.Status.ToString()));
    }

    [Test]
    public void Map_OrderItem_To_GetItemDto()
    {
        // arrange
        var item = Fixture.Create<OrderItem>();

        // act
        var actual = item.Adapt<OrderGetItemDto>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.Quantity, Is.EqualTo(item.Quantity));
        Assert.That(actual.CoffeeId, Is.EqualTo(item.CoffeeId));
        Assert.That(actual.CoffeeName, Is.EqualTo(item.Coffee.Name));
        Assert.That(actual.CoffeeType, Is.EqualTo(item.Coffee.Type));
    }


    [Test]
    public void Map_CreateDto_To_Command()
    {
        // arrange
        var item = Fixture.Create<OrderCreateDto>();

        // act
        var actual = item.Adapt<OrderCreate.Command>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.Name, Is.EqualTo(item.Name)); 
    }

    [Test]
    public void Map_CreateItemDto_To_CommandItem()
    {
        // arrange
        var item = Fixture.Create<OrderCreateItemDto>();

        // act
        var actual = item.Adapt<OrderCreate.CommandItem>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.Quantity, Is.EqualTo(item.Quantity));
        Assert.That(actual.CoffeeId, Is.EqualTo(item.CoffeeId));
    }

    [Test]
    public void Map_UpdateDto_To_Command()
    {
        // arrange
        var item = Fixture.Create<OrderUpdateDto>();

        // act
        var actual = item.Adapt<OrderUpdate.Command>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.Id, Is.EqualTo(item.Id));
        Assert.That(actual.Name, Is.EqualTo(item.Name)); 
    }

    [Test]
    public void Map_UpdateItemDto_To_CommandItem()
    {
        // arrange
        var item = Fixture.Create<OrderUpdateItemDto>();

        // act
        var actual = item.Adapt<OrderUpdate.CommandItem>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.Quantity, Is.EqualTo(item.Quantity));
        Assert.That(actual.CoffeeId, Is.EqualTo(item.CoffeeId));
    }
}
