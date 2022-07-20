using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Infrastructure.MySQL;

namespace Coffeeshop.Infrastructure;

internal class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(CoffeeContext coffeeContext)
    {
        _repositories = new List<IRepository>();
        _coffeeContext = coffeeContext;
    }

    private readonly IList<IRepository> _repositories;
    private readonly CoffeeContext _coffeeContext;

    public IAsyncRepository<T> AsyncRepository<T>() 
        where T : class, IEntity<long>
    {
        var repository = new GenericRepository<T>(_coffeeContext);
        _repositories.Add(repository);
        return repository;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _coffeeContext.SaveChangesAsync(cancellationToken);
    }
}
