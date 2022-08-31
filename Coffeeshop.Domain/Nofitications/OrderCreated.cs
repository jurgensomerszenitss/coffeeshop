using Coffeeshop.Domain.Commands;
using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Coffeeshop.Domain.Notifications;

public static class OrderCreated
{
    /// notifications are domain events
    /// they don't return any value, but only execute additional logic required by the domain
    public record Notification(long Id) : INotification;

    public class Handler : INotificationHandler<Notification>
    {
        public Handler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public async Task Handle(Notification request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.AsyncRepository<Order>();
            var order = await repository.GetAsync(request.Id, p => p.Items);

            if (order != null)
            {
                var idQuantities = order.Items.ToDictionary(x => x.CoffeeId, x => x.Quantity);
                await _mediator.Send(new CoffeePatch.Command(idQuantities));              
            }
        }
    }
}
