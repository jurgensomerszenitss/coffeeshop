using Coffeeshop.Domain.Models;

namespace Coffeeshop.Domain.Queries.Filters;

/// <summary>
/// Builder pattern implementation for easy where clauses
/// Methods have simple implementation, and only 1 where if the variable has a query    
/// </summary>
internal static class CoffeeFilters
{   
    public static IQueryable<Coffee> WhereName(this IQueryable<Coffee> query, string? name)
    {
        if(!string.IsNullOrWhiteSpace(name)) // condition to add the where
        {
            query = query.Where(x => x.Name.StartsWith(name, StringComparison.InvariantCultureIgnoreCase)); // add where clause
        }

        return query; // single exit point
    }

    public static IQueryable<Coffee> WhereType(this IQueryable<Coffee> query, string? type)
    {
        if (!string.IsNullOrWhiteSpace(type))
        {
            query = query.Where(x => x.Type.StartsWith(type, StringComparison.InvariantCultureIgnoreCase));
        }

        return query;
    }
}
