using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using Coffeeshop.Domain.Queries.Filters;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Coffeeshop.Domain.Queries;

public static class CoffeeSearch
{
    public record Query(string Name, string Type) : IRequest<IEnumerable<Coffee>>;

    // Handler receive specific query and return domain models (or list of domain models)
    public class Handler : IRequestHandler<Query, IEnumerable<Coffee>>
    {
        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<IEnumerable<Coffee>> Handle(Query request, CancellationToken cancellationToken)
        {
            using var repository = _unitOfWork.AsyncRepository<Coffee>(); // important : using!

            // construct filter
            var filter = (IQueryable<Coffee> q) => q
                .Include(p => p.Supplier)
                .WhereName(request.Name)
                .WhereType(request.Type);

            // query and return
            var result = await repository.QueryAsync(filter);
            return result;
        }
    }
}
