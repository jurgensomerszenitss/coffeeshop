using Coffeeshop.Domain.Models;

namespace Coffeeshop.Domain.Queries.Filters;

/// <summary>
/// Builder pattern implementation for easy where clauses
/// Methods have simple implementation, and only 1 where if the variable has a query    
/// </summary>
internal static class OrderItemFilters
{   
    public static IQueryable<OrderItem> WhereCoffeeId(this IQueryable<OrderItem> query, long? coffeeId)
    {
        if(coffeeId.HasValue) // condition to add the where
        {
            query = query.Where(x => x.CoffeeId == coffeeId.Value); // add where clause
        }

        return query; // single exit point
    } 
}
