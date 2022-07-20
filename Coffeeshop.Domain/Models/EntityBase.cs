using Coffeeshop.Domain.Interfaces;

namespace Coffeeshop.Domain.Models;

public abstract class EntityBase : IEntity<long>
{
    public long Id { get; set; }
}
