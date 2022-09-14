using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using Coffeeshop.Domain.Notifications;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Coffeeshop.Domain.Commands;

public static class OrderCreate
{
    /// commands are immutable, so record (class) is the best type to use   
    /// commands don't contain model objects, or any other objects, but have to be considered as an instruction to alter the domain
    public record Command(string Name, ICollection<CommandItem> Items) :IRequest<long>;
    public record CommandItem (long CoffeeId, long Quantity);

    public class Handler : IRequestHandler<Command, long>
    {
        public Handler(IUnitOfWork unitOfWork, IMediator mediator, ILogger<Handler> logger)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _logger = logger;
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly ILogger<Handler> _logger;

        public async Task<long> Handle(Command request, CancellationToken cancellationToken)
        {
            using var repository = _unitOfWork.AsyncRepository<Order>();
            var entity = request.Adapt<Order>();
            entity.Date = DateTime.Now;
            entity.Status = OrderStatus.Pending;
            await repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new OrderCreated.Notification(entity.Id)); 
            return entity.Id;

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
