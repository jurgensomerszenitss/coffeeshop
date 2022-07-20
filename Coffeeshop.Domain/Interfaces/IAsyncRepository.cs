namespace Coffeeshop.Domain.Interfaces;

public interface IAsyncRepository<TEntity> : IRepository, IDisposable
    where TEntity : class,IEntity<long>
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<bool> UpdateAsync(TEntity entity);
    Task<bool> RemoveAsync(long id);
    Task<TEntity?> GetAsync(long id);
    Task<IEnumerable<TEntity>> QueryAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? filter = null);
    long GetId(TEntity entity);
}