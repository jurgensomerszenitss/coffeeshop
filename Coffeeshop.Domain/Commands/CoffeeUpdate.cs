using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using Mapster;
using MediatR;

namespace Coffeeshop.Domain.Commands;

public static class CoffeeUpdate
{
    public record Command(long Id, string Name, string Type, string SupplierId) :IRequest<bool> ;

    public class Handler : IRequestHandler<Command, bool>
    {
        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            using var repository = _unitOfWork.AsyncRepository<Coffee>();
            var entity = request.Adapt<Coffee>();
            await repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
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
