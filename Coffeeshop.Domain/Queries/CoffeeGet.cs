using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using MediatR;

namespace Coffeeshop.Domain.Queries;

public static class CoffeeGet // static class, containing only other classes
{
    
    // queries are immutable, so record (class) is the best type to use
    public record Query(long Id) : IRequest<Coffee?>;

    // Handler receive specific query and return domain models (or list of domain models)
    public class Handler : IRequestHandler<Query, Coffee?>
    {
        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<Coffee?> Handle(Query request, CancellationToken cancellationToken)
        {
            using var repository = _unitOfWork.AsyncRepository<Coffee>();
            var entity = await repository.GetAsync(request.Id);
            return entity;
        }
    }
}
