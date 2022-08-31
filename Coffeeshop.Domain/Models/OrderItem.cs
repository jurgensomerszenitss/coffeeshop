namespace Coffeeshop.Domain.Models;

/// <summary>
///  simple classes, no methods or logic in here
/// </summary>
public class OrderItem : EntityBase
{
    public long Quantity { get; set; }

    public long OrderId { get; set; }
    public long CoffeeId { get; set; }

    public virtual Order? Order { get; set; }
    public virtual Coffee? Coffee { get; set; }
}
