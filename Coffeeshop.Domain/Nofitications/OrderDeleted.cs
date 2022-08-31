using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Coffeeshop.Domain.Notifications;

public static class OrderDeleted
{
    /// notifications are domain events
    /// they don't return any value, but only execute additional logic required by the domain
    public record Notification(long Id) : INotification;

    public class Handler : INotificationHandler<Notification>
    {
        public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Handler> _logger;

        public async Task Handle(Notification request, CancellationToken cancellationToken)
        {
            var orderRepository = _unitOfWork.AsyncRepository<Order>();
            var coffeeRepository = _unitOfWork.AsyncRepository<Coffee>();
            var order = await orderRepository.GetAsync(request.Id, p => p.Items);

            if (order != null)
            {
                /// We update the available stock
                foreach (var item in order.Items)
                {
                    var coffee = await coffeeRepository.GetAsync(item.CoffeeId);                   
                    coffee.Quantity += item.Quantity;
                    await coffeeRepository.UpdateAsync(coffee);

                    _logger.LogInformation($"Stock quantity updated for coffee ${coffee.Id} - ${coffee.Name}");
                }

                await coffeeRepository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
