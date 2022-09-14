namespace Coffeeshop.Domain.Models;

/// <summary>
///  enum with flags and bitwise operator
/// </summary>
[Flags]
public enum OrderStatus
{
    Pending = 1 << 0,
    Ready = 1 << 1,
    Delivered = 1 << 2
}
