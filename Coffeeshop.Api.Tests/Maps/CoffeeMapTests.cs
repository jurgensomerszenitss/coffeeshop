using Coffeeshop.Api.Dto;
using Coffeeshop.Domain.Commands;
using Coffeeshop.Domain.Models;
using Coffeeshop.Domain.Queries;

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
        Assert.That(actual.SupplierId, Is.EqualTo(item.SupplierId));
        Assert.That(actual.SupplierName, Is.EqualTo(item.Supplier?.Name));
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
        Assert.That(actual.SupplierId, Is.EqualTo(item.SupplierId));
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
        Assert.That(actual.SupplierId, Is.EqualTo(item.SupplierId));
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
        Assert.That(actual.SupplierId, Is.EqualTo(item.SupplierId));
    }
}
