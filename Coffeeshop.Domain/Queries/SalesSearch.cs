using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using Coffeeshop.Domain.Queries.Filters;
using Mapster;
using MediatR;

namespace Coffeeshop.Domain.Queries;

/// <summary>
/// Example of a query, not linked strongly to the domain
/// </summary>
public static class SalesSearch
{
    public record Query(string? Type = null) : IRequest<IEnumerable<Response>>;
    public record Response(long CoffeeId, string CoffeeName, long Quantity);

    // Handler receive specific query and return domain models (or list of domain models)
    public class Handler : IRequestHandler<Query, IEnumerable<Response>>
    {
        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<IEnumerable<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            using var coffeeRepository = _unitOfWork.AsyncRepository<Coffee>(); // important : using!

            // query 
            var coffeeList = await coffeeRepository.QueryAsync((q) => q.WhereType(request.Type), c => c.Orders);

            return coffeeList.Select(c => c.Adapt<Response>());
        }
    }

    public class Map : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Coffee, Response>()
                .Map(d => d.CoffeeId, s => s.Id)
                .Map(d => d.CoffeeName, s => s.Name)
                .Map(d => d.Quantity, s => s.Orders.Sum(o => o.Quantity));
        }
    }
}
