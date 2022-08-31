using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using Coffeeshop.Domain.Notifications;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Coffeeshop.Domain.Commands;

public static class CoffeeCreate
{
    /// commands are immutable, so record (class) is the best type to use   
    /// commands don't contain model objects, or any other objects, but have to be considered as an instruction to alter the domain
    public record Command(string Name, string Type, long Quantity) :IRequest<long>;

    public class Handler : IRequestHandler<Command, long>
    {
        public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Handler> _logger;

        public async Task<long> Handle(Command request, CancellationToken cancellationToken)
        {
            using (_logger.BeginScope($"More coffee will be created : {request.Name}")) // -- log scope
            {
                using var repository = _unitOfWork.AsyncRepository<Coffee>();
                var entity = request.Adapt<Coffee>();
                await repository.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
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
