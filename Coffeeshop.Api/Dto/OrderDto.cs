namespace Coffeeshop.Api.Dto;

// all dto's are immutable record types
public record OrderSearchDto(long Id, string Name, DateTime? Date);
public record OrderQueryDto(string? Name);
public record OrderGetDto(long Id, string Name, DateTime Date, IEnumerable<OrderGetItemDto>? Items);
public record OrderGetItemDto(long CoffeeId, string CoffeeName, string CoffeeType, long Quantity);
public record OrderCreateDto(string? Name, IEnumerable<OrderCreateItemDto>? Items);
public record OrderCreateItemDto(long CoffeeId, long Quantity);
public record OrderUpdateDto(long Id, string? Name, IEnumerable<OrderUpdateItemDto>? Items);
public record OrderUpdateItemDto(long CoffeeId, long Quantity);
public record OrderDeleteDto(long Id);
public record OrderPatchDto(string? Name);