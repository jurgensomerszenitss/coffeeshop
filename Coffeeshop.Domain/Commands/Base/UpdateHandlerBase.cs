using Coffeeshop.Domain.Interfaces;
using Mapster;

namespace Coffeeshop.Domain.Commands.Base;

public abstract class UpdateHandlerBase<TCommand, TEntity>
    where TEntity : class, IEntity<long>
{
    protected UpdateHandlerBase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private readonly IUnitOfWork _unitOfWork;

    protected virtual async Task<UpdateResult> Handle(long id, TCommand request, CancellationToken cancellationToken)
    {
        using var repository = _unitOfWork.AsyncRepository<TEntity>();
        var existing = await Load(id, repository);
        if (existing == null)
            return UpdateResult.NotFound;
        
        var entity = request.Adapt(existing);
        var success = await repository.UpdateAsync(entity);
        if (success)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        return UpdateResult.Success;
    } 

    protected virtual async Task<TEntity?> Load(long id, IAsyncRepository<TEntity> repository)
    {
        return await repository.GetAsync(id);
    }
}
