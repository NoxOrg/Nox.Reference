namespace Nox.Reference.Common;

public class LookupHandler<TEntity>
{
    public IEnumerable<TEntity> Lookup(
        IEnumerable<TEntity> entities,
        params Func<TEntity, bool>[] searches)
    {
        if (searches.Length == 0)
        {
            return entities;
        }

        foreach (var search in searches)
        {
            entities = entities.Where(search);
        }

        return entities;
    }
}