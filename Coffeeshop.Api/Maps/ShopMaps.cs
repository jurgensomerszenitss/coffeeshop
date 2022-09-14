using Coffeeshop.Api.Dto;
using Coffeeshop.Domain.Models;
using Mapster;

namespace Coffeeshop.Api.Maps;

// defines maps between the model and dto's and the dto's and commands
public class ShopMaps : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // queries
        config.NewConfig<ShopStatus, ShopGetStatusDto>()
            .Map(d => d.Status, s => s.ToString()); 
    }
}

