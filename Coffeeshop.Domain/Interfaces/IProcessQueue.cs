using Coffeeshop.Domain.Models;

namespace Coffeeshop.Domain.Interfaces;

public interface IProcessQueue
{
    Task Schedule(long id);
    Task<long?> TakeNext();
}
