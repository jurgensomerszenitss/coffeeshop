namespace Coffeeshop.Api.Dto;

// all dto's are immutable record types
public record CoffeeSearchDto(long Id, string Name, string Type, long SupplierId, string SupplierName);
public record CoffeeQueryDto(string? Name);
public record CoffeeGetDto(long Id, string Name, string Type, long SupplierId);
public record CoffeeCreateDto(string Name, string Type, long SupplierId);
public record CoffeeUpdateDto(long Id, string Name, string Type, long SupplierId);
 