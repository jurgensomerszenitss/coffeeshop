using Coffeeshop.Domain.Commands;
using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using MediatR;

namespace Coffeeshop.Domain.Notifications;

public static class OrderCreated
{
    /// notifications are domain events
    /// they don't return any value, but only execute additional logic required by the domain <summary>
    /// notifications are domain events
    /// In this example, we update the stock (reserve it), and queue the order for processing using Redis
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
                var idQuantities = order.Items.GroupBy(x => x.CoffeeId).ToDictionary(x => x.Key, x => x.Sum(i => i.Quantity));
                await _mediator.Send(new CoffeePatch.Command(idQuantities));
                await _mediator.Send(new OrderSchedule.Command(request.Id));
            }
        }
    }
}
