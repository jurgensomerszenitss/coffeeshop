using Coffeeshop.Api.Dto;
using Coffeeshop.Domain.Commands;
using Coffeeshop.Domain.Models;
using Coffeeshop.Domain.Queries;
using NUnit.Framework;

namespace Coffeeshop.Api.Tests.Maps;

internal class CoffeeMapTests : TestBase
{
    [Test]
    public void Map_QueryDto_To_Query()
    {
        // arrange
        var item = Fixture.Create<CoffeeQueryDto>();

        // act
        var actual = item.Adapt<CoffeeSearch.Query>();

        // assert
        Assert.IsNotNull(actual); 
        Assert.That(actual.Name, Is.EqualTo(item.Name));
        Assert.That(actual.Type, Is.EqualTo(item.Type));
    }

    [Test]
    public void Map_Coffee_To_SearchDto()
    {
        // arrange
        var item = Fixture.Create<Coffee>();

        // act
        var actual = item.Adapt<CoffeeSearchDto>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.Id, Is.EqualTo(item.Id));
        Assert.That(actual.Name, Is.EqualTo(item.Name));
        Assert.That(actual.Type, Is.EqualTo(item.Type));        
    }


    [Test]
    public void Map_Coffee_To_GetDto()
    {
        // arrange
        var item = Fixture.Create<Coffee>();

        // act
        var actual = item.Adapt<CoffeeGetDto>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.Id, Is.EqualTo(item.Id));
        Assert.That(actual.Name, Is.EqualTo(item.Name));
        Assert.That(actual.Type, Is.EqualTo(item.Type));
        Assert.That(actual.Quantity, Is.EqualTo(item.Quantity));
    }

    [Test]
    public void Map_CreateDto_To_Command()
    {
        // arrange
        var item = Fixture.Create<CoffeeCreateDto>();

        // act
        var actual = item.Adapt<CoffeeCreate.Command>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.Name, Is.EqualTo(item.Name));
        Assert.That(actual.Type, Is.EqualTo(item.Type));
        Assert.That(actual.Quantity, Is.EqualTo(item.Quantity));
    }

    [Test]
    public void Map_UpdateDto_To_Command()
    {
        // arrange
        var item = Fixture.Create<CoffeeUpdateDto>();

        // act
        var actual = item.Adapt<CoffeeUpdate.Command>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.Id, Is.EqualTo(item.Id));
        Assert.That(actual.Name, Is.EqualTo(item.Name));
        Assert.That(actual.Type, Is.EqualTo(item.Type));
        Assert.That(actual.Quantity, Is.EqualTo(item.Quantity));
    }

    [Test]
    public void Map_SalesSearchResponse_To_Dto()
    {
        // arrange
        var item = Fixture.Create<SalesSearch.Response>();

        // act
        var actual = item.Adapt<CoffeeSalesDto>();

        // assert
        Assert.IsNotNull(actual);
        Assert.That(actual.CoffeeId, Is.EqualTo(item.CoffeeId));
        Assert.That(actual.CoffeeName, Is.EqualTo(item.CoffeeName));
        Assert.That(actual.Quantity, Is.EqualTo(item.Quantity));
    }
}
