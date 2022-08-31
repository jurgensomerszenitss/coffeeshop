using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using MediatR;

namespace Coffeeshop.Domain.Queries;

public static class TypeSearch // static class, containing only other classes
{
    
    // queries are immutable, so record (class) is the best type to use
    public record Query() : IRequest<IEnumerable<string>>;

    public class Handler : IRequestHandler<Query, IEnumerable<string>>
    {
        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<IEnumerable<string>> Handle(Query request, CancellationToken cancellationToken)
        {
            using var repository = _unitOfWork.AsyncRepository<Coffee>();
            var list = await repository.QueryAsync();
            return list.Select(x => x.Type).Distinct();
        }
    }
}
