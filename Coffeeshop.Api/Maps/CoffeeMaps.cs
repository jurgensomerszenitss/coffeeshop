using Coffeeshop.Api.Dto;
using Coffeeshop.Domain.Commands;
using Coffeeshop.Domain.Models;
using Coffeeshop.Domain.Queries;
using Mapster;

namespace Coffeeshop.Api.Maps;

// defines maps between the model and dto's and the dto's and commands
public class CoffeeMaps : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // queries
        config.NewConfig<CoffeeQueryDto, CoffeeSearch.Query>();

        // model
        config.NewConfig<Coffee, CoffeeSearchDto>();

        // commands
        config.NewConfig<CoffeeCreateDto, CoffeeCreate.Command>();
        config.NewConfig<CoffeeUpdateDto, CoffeeUpdate.Command>();
    }
}

