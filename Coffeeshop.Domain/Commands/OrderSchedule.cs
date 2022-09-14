using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using Mapster;
using MediatR;

namespace Coffeeshop.Domain.Commands;

public static class OrderSchedule
{
    public record Command(long Id) :IRequest;

    public class Handler : IRequestHandler<Command>
    {
        public Handler(IProcessQueue processRepository) 
        {
            _processRepository = processRepository;
        }

        private readonly IProcessQueue _processRepository;

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            await _processRepository.Schedule(request.Id);
            return new Unit();
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
