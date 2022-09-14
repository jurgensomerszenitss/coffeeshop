using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using Mapster;
using MediatR;

namespace Coffeeshop.Domain.Commands;

public static class ShopPatch
{
    public record Command(ShopStatus Status) :IRequest<UpdateResult>;

    public class Handler : IRequestHandler<Command, UpdateResult>
    {
        public Handler(IShopCache shopRepository) 
        {
            _shopRepository = shopRepository;
        }

        private readonly IShopCache _shopRepository;

        public async Task<UpdateResult> Handle(Command request, CancellationToken cancellationToken)
        {
            await _shopRepository.SetStatus(request.Status);
            return UpdateResult.Success;
        }
    }

    public class Map : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Command, Order>()
                .IgnoreNullValues(true);
        }
    }
}
