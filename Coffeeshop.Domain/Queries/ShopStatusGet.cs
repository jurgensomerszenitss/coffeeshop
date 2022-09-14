using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using MediatR;

namespace Coffeeshop.Domain.Queries;

public static class ShopStatusGet // static class, containing only other classes
{
    
    // queries are immutable, so record (class) is the best type to use
    public record Query() : IRequest<ShopStatus>;

    // Handler receive specific query and return domain models (or list of domain models)
    public class Handler : IRequestHandler<Query, ShopStatus>
    {
        public Handler(IShopCache shopRepository)
        {
            _shopRepository = shopRepository;
        }

        private readonly IShopCache _shopRepository;

        public async Task<ShopStatus> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _shopRepository.GetStatus();
        }
    }
}
