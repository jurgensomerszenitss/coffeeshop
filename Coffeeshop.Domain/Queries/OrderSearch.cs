using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using Coffeeshop.Domain.Queries.Filters;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Coffeeshop.Domain.Queries;

public static class OrderSearch
{
    public record Query(string Name, string Type) : IRequest<IEnumerable<Order>>;

    // Handler receive specific query and return domain models (or list of domain models)
    public class Handler : IRequestHandler<Query, IEnumerable<Order>>
    {
        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<IEnumerable<Order>> Handle(Query request, CancellationToken cancellationToken)
        {
            using var repository = _unitOfWork.AsyncRepository<Order>(); // important : using!

            // construct filter
            var filter = (IQueryable<Order> q) => q
                .WhereName(request.Name);

            // query and return
            var result = await repository.QueryAsync(filter);
            return result;
        }
    } 
}
