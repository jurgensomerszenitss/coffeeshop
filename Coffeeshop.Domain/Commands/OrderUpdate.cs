using Coffeeshop.Domain.Commands.Base;
using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using Coffeeshop.Domain.Notifications;
using Mapster;
using MediatR;

namespace Coffeeshop.Domain.Commands;

public static class OrderUpdate
{
    public record Command(long Id, string Name, ICollection<CommandItem> Items) :IRequest<UpdateResult>;
    public record CommandItem(long CoffeeId, long Quantity);

    public class Handler : UpdateHandlerBase<Command,Order>, IRequestHandler<Command, UpdateResult>
    {
        public Handler(IUnitOfWork unitOfWork, IMediator mediator) : base(unitOfWork)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;

        public async Task<UpdateResult> Handle(Command request, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new OrderDeleted.Notification(request.Id));
            var result = await base.Handle(request.Id, request, cancellationToken);
            await _mediator.Publish(new OrderCreated.Notification(request.Id));
            return result;
        }
    }

    public class Map : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Command, Order>();
            config.NewConfig<CommandItem, OrderItem>();
        }
    }
}
