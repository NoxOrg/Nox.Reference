using Microsoft.EntityFrameworkCore;

namespace Nox.Reference.Data.Common;

public static class NoxReferenceQueryExtensions
{
    public static IQueryable<T> IncludeAll<T>(this IQueryable<T> queryable) where T : class
    {
        var entityType = queryable.GetType()
            .GenericTypeArguments[0];

        var navigationProperties = entityType.GetProperties()
            .Where(p => p.PropertyType.IsGenericType
                && p.PropertyType.GetGenericTypeDefinition() == typeof(IReadOnlyList<>));

        foreach (var navigationProperty in navigationProperties)
        {
            queryable = queryable.Include(navigationProperty.Name);
            queryable.IncludeAll();
        }

        return queryable;
    }
}