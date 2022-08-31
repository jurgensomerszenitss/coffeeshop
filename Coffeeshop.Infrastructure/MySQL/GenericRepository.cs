using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using Mapster;
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

    public async Task<T?> GetAsync(long id, params Expression<Func<T,object>>[] includes)
    {
        var query = _coffeeContext.Set<T>().AsNoTracking();
        if (includes.Any())
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return await query.SingleOrDefaultAsync(x => x.Id == id);
    }

    public long GetId(T entity)
    {
        return entity.Id;
    }

    public async Task<IEnumerable<T>> QueryAsync(Func<IQueryable<T>,  IQueryable<T>>? filter = null, params Expression<Func<T, object>>[] includes)
    {
        var query = _coffeeContext.Set<T>().AsNoTracking();
        if (includes.Any())
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (filter != null)
            return await Task.FromResult(filter(query).ToList());
        else
            return await Task.FromResult(query.AsEnumerable());
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
        //if (disposing)
        //{

        //}
    }
}
