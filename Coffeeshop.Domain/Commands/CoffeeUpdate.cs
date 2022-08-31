using Coffeeshop.Domain.Commands.Base;
using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using Mapster;
using MediatR;

namespace Coffeeshop.Domain.Commands;

public static class CoffeeUpdate
{
    public record Command(long Id, string Name, string Type, long Quantity) :IRequest<UpdateResult>;

    public class Handler : UpdateHandlerBase<Command, Coffee>, IRequestHandler<Command, UpdateResult>
    {
        public Handler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<UpdateResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return base.Handle(request.Id, request, cancellationToken);
        }
    }

    public class Map : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Command, Coffee>();
        }
    }
}
