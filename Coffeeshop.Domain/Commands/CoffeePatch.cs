using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Coffeeshop.Domain.Commands;

/// <summary>
/// A patch is a partial update
/// </summary>
public static class CoffeePatch
{
    public record Command(IEnumerable<KeyValuePair<long,long>> IdQuantityPairs) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Handler> _logger;

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.AsyncRepository<Coffee>();

            foreach (var idQuantity in request.IdQuantityPairs)
            {
                var coffee = await repository.GetAsync(idQuantity.Key);
                coffee.Quantity -= idQuantity.Value;
                await repository.UpdateAsync(coffee);

                _logger.LogInformation($"Stock quantity updated for coffee ${coffee.Id} - ${coffee.Name}");
            }
            await repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        } 
    } 
}
