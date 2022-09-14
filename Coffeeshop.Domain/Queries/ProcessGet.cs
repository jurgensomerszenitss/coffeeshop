using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using MediatR;

namespace Coffeeshop.Domain.Queries;

public static class ProcessGet // static class, containing only other classes
{
    
    // queries are immutable, so record (class) is the best type to use
    public record Query() : IRequest<Order?>;

    // Handler receive specific query and return domain models (or list of domain models)
    public class Handler : IRequestHandler<Query, Order?>
    {
        public Handler(IUnitOfWork unitOfWork, IProcessQueue processRepository)
        {
            _processRepository = processRepository;
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IProcessQueue _processRepository;

        public async Task<Order?> Handle(Query request, CancellationToken cancellationToken)
        {
            var id = await _processRepository.TakeNext();
            if (id.HasValue)
            {
                using var repository = _unitOfWork.AsyncRepository<Order>();
                var entity = await repository.GetAsync(id.Value, x => x.Items);
                return entity;
            }

            return default(Order);
        }
    }
}
