using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using Mapster;
using MediatR;

namespace Coffeeshop.Domain.Commands;

public static class OrderPatch
{
    public record Command(long Id, string? Name) :IRequest<UpdateResult>;

    public class Handler : IRequestHandler<Command, UpdateResult>
    {
        public Handler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<UpdateResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.AsyncRepository<Order>();
            var entity = await repository.GetAsync(request.Id);
            if (entity == null) return UpdateResult.NotFound;

            request.Adapt(entity);
            var success = await repository.UpdateAsync(entity);
            if (success)
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return UpdateResult.Success;
        }
    }

    public class Map : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Command, Order>()
                .IgnoreNullValues(true);
        }
    }
}
