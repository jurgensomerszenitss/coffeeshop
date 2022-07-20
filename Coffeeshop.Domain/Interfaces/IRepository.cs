namespace Coffeeshop.Domain.Interfaces;

public interface IRepository
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
}