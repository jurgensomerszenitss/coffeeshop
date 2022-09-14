namespace Coffeeshop.Domain.Models;

/// <summary>
///  simple classes, no methods or logic in here
/// </summary>
public class Order : EntityBase
{
    public DateTime Date { get; set; }
    public string Name { get; set; } = string.Empty;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
