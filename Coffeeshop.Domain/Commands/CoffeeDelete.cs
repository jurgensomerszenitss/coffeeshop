using Coffeeshop.Domain.Commands.Base;
using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using Coffeeshop.Domain.Queries.Filters;
using MediatR;

namespace Coffeeshop.Domain.Commands;

public static class CoffeeDelete
{
    public record Command(long Id) :IRequest<DeleteResult>;

    public class Handler : DeleteHandlerBase<Coffee>, IRequestHandler<Command, DeleteResult>
    {
        public Handler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<DeleteResult> Handle(Command request, CancellationToken cancellationToken)
        {
            if(await HasOrderItemDependency(request.Id))
            {
                return DeleteResult.Forbidden;
            }
            return await base.Handle(request.Id, cancellationToken);
        }

        private async Task<bool> HasOrderItemDependency(long id)
        {
            var orderItemRepository = _unitOfWork.AsyncRepository<OrderItem>();
            return (await orderItemRepository.QueryAsync( (q)=> q.WhereCoffeeId(id))).Any();
        }
    } 
}
