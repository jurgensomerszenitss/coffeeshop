namespace Coffeeshop.Api.Dto;

// all dto's are immutable record types
public record OrderSearchDto(long Id, string Name, DateTime? Date, string Status);
public record OrderQueryDto(string? Name);
public record OrderGetDto(long Id, string Name, DateTime Date, string Status, IEnumerable<OrderGetItemDto>? Items);
public record OrderGetItemDto(long Id, long CoffeeId, string CoffeeName, string CoffeeType, long Quantity, string Status);
public record OrderCreateDto(string? Name, IEnumerable<OrderCreateItemDto>? Items);
public record OrderCreateItemDto(long CoffeeId, long Quantity);
public record OrderUpdateDto(long Id, string? Name, IEnumerable<OrderUpdateItemDto>? Items);
public record OrderUpdateItemDto(long Id, long CoffeeId, long Quantity);
public record OrderDeleteDto(long Id);
public record OrderPatchDto(string? Name);