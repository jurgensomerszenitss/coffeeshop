using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using Mapster;
using MediatR;

namespace Coffeeshop.Domain.Commands;

public static class CoffeeCreate
{
    /// commands are immutable, so record (class) is the best type to use   
    /// commands don't contain model objects, or any other objects, but have to be considered as an instruction to alter the domain
    public record Command(string Name, string Type, string SupplierId) :IRequest<long>;

    public class Handler : IRequestHandler<Command, long>
    {
        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<long> Handle(Command request, CancellationToken cancellationToken)
        {
            using var repository = _unitOfWork.AsyncRepository<Coffee>();
            var entity = request.Adapt<Coffee>();
            await repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return entity.Id;
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
