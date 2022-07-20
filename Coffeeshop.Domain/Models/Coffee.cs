namespace Coffeeshop.Domain.Models;

/// <summary>
///  simple classes, no methods or logic in here
/// </summary>
public class Coffee : EntityBase
{
    public string Name { get; set; } =  string.Empty;
    public string Type { get; set; } = string.Empty;
    public long SupplierId { get; set; }
    public virtual Supplier? Supplier { get; set; }
}
