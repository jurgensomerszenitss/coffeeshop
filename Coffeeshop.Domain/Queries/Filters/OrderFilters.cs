using Coffeeshop.Domain.Models;

namespace Coffeeshop.Domain.Queries.Filters;

/// <summary>
/// Builder pattern implementation for easy where clauses
/// Methods have simple implementation, and only 1 where if the variable has a query    
/// </summary>
internal static class OrderFilters
{   
    public static IQueryable<Order> WhereName(this IQueryable<Order> query, string? name)
    {
        if(!string.IsNullOrWhiteSpace(name)) // condition to add the where
        {
            query = query.Where(x => x.Name.StartsWith(name)); // add where clause
        }

        return query; // single exit point
    } 
}
