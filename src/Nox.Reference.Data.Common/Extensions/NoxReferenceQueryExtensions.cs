using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nox.Reference.Data.Common;

public static class NoxReferenceQueryExtensions
{
    public static IQueryable<T> IncludeAll<T>(this IQueryable<T> queryable) where T : class
    {
        var entityType = queryable.GetType()
            .GenericTypeArguments[0];

        var navigationProperties = entityType.GetProperties()
            .Where(p =>
                // force-ignore
                !Attribute.IsDefined(p, typeof(NotMappedAttribute)) &&
                // one-to-many
                ((p.PropertyType.IsGenericType &&
                p.PropertyType.GetGenericTypeDefinition() == typeof(IReadOnlyList<>)) ||
                // one-to-one
                typeof(INoxReferenceEntity).IsAssignableFrom(p.PropertyType)));

        foreach (var navigationProperty in navigationProperties)
        {
            queryable = queryable.Include(navigationProperty.Name);
        }

        return queryable;
    }
}