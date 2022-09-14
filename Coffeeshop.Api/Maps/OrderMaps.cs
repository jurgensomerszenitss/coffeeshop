using Coffeeshop.Api.Dto;
using Coffeeshop.Domain.Commands;
using Coffeeshop.Domain.Models;
using Coffeeshop.Domain.Queries;
using Mapster;

namespace Coffeeshop.Api.Maps;

// defines maps between the model and dto's and the dto's and commands
public class OrderMaps : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // queries
        config.NewConfig<OrderQueryDto, OrderSearch.Query>();

        // model
        config.NewConfig<Order, OrderSearchDto>()
            .Map(d => d.Status, s => s.Status.ToString());
        config.NewConfig<Order, OrderGetDto>()
            .Map(d => d.Status, s => s.Status.ToString());
        config.NewConfig<OrderItem, OrderGetItemDto>();

        // commands
        config.NewConfig<OrderCreateDto, OrderCreate.Command>();
        config.NewConfig<OrderCreateItemDto, OrderCreate.CommandItem>();
        config.NewConfig<OrderUpdateDto, OrderUpdate.Command>();
        config.NewConfig<OrderUpdateItemDto, OrderUpdate.CommandItem>();
    }
}

