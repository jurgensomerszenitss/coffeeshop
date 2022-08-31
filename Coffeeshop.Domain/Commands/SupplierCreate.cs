using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using Coffeeshop.Domain.Notifications;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Coffeeshop.Domain.Commands;

public static class SupplierCreate
{
    /// commands are immutable, so record (class) is the best type to use   
    /// commands don't contain model objects, or any other objects, but have to be considered as an instruction to alter the domain
    public record Command(string Name) :IRequest<long>;

    public class Handler : IRequestHandler<Command, long>
    {
        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<long> Handle(Command request, CancellationToken cancellationToken)
        {
            using var repository = _unitOfWork.AsyncRepository<Order>();
            var entity = request.Adapt<Order>();
            await repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }

    public class Map : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Command, Order>();
        }
    }
}
