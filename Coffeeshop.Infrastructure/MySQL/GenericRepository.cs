using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Coffeeshop.Infrastructure.MySQL;


internal class GenericRepository<T> : IAsyncRepository<T>
    where T : class, IEntity<long>
{
    public GenericRepository(CoffeeContext coffeeContext)
    {
        _coffeeContext = coffeeContext;
    }

    private readonly CoffeeContext _coffeeContext;

    ~GenericRepository()
    {
        Dispose(false);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _coffeeContext.AddAsync(entity);
        return entity;
    }

    public async Task<T?> GetAsync(long id)
    {
        return await _coffeeContext.FindAsync<T>(id);
    }

    public long GetId(T entity)
    {
        return entity.Id;
    }

    public async Task<IEnumerable<T>> QueryAsync(Func<IQueryable<T>, IQueryable<T>>? filter = null)
    {
        if (filter != null)
            return await Task.FromResult(filter(_coffeeContext.Set<T>()).ToList());
        else
            return await Task.FromResult(_coffeeContext.Set<T>().AsEnumerable());
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var entity = _coffeeContext.Find<T>(id);
        if (entity != null)
        {
            _coffeeContext.Remove(entity);
            return true;
        }
        await Task.Yield();
        return false;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _coffeeContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _coffeeContext.Update(entity);
        return await Task.FromResult(true);
    }

    public virtual void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {

        }
    }
}
