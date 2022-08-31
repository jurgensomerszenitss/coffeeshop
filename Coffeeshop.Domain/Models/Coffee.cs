namespace Coffeeshop.Domain.Models;

/// <summary>
///  simple classes, no methods or logic in here
/// </summary>
public class Coffee : EntityBase
{
    public string Name { get; set; } =  string.Empty;
    public string Type { get; set; } = string.Empty;
    public long Quantity { get; set; } = 0;
    public virtual ICollection<OrderItem> Orders { get; set; } = new List<OrderItem>();
}
