using Coffeeshop.Domain.Models;

namespace Coffeeshop.Domain.Interfaces;

public interface IShopCache
{
    Task<ShopStatus> GetStatus();
    Task SetStatus(ShopStatus status);
}
