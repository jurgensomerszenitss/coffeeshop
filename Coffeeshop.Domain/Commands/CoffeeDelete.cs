using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using MediatR;

namespace Coffeeshop.Domain.Commands;

public static class CoffeeDelete
{
    public record Command(long Id) :IRequest<bool> ;

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
            await repository.RemoveAsync(request.Id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    } 
}
