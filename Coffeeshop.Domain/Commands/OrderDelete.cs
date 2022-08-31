using Coffeeshop.Domain.Commands.Base;
using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using MediatR;

namespace Coffeeshop.Domain.Commands;

public static class OrderDelete
{
    public record Command(long Id) :IRequest<DeleteResult> ;

    public class Handler : DeleteHandlerBase<Order>, IRequestHandler<Command, DeleteResult>
    {
        public Handler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<DeleteResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return base.Handle(request.Id, cancellationToken); 
        }
    } 
}
