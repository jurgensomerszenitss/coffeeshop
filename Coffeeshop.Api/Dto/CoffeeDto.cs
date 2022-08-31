namespace Coffeeshop.Api.Dto;

// all dto's are immutable record types
public record CoffeeSearchDto(long Id, string Name, string Type);
public record CoffeeQueryDto(string? Name, string? Type);
public record CoffeeGetDto(long Id, string Name, string Type, long Quantity);
public record CoffeeCreateDto(string? Name, string? Type, long Quantity);
public record CoffeeUpdateDto(long Id, string? Name, string? Type, long Quantity);
public record CoffeeDeleteDto(long Id);
public record CoffeeSalesDto(long CoffeeId, string CoffeeName, long Quantity);