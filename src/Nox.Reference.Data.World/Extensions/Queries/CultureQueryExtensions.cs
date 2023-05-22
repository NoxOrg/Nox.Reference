namespace Nox.Reference.Data.World.Extensions.Queries;

public static class CultureQueryExtensions
{
    public static Culture Get(this IQueryable<Culture> query, string code)
    {
        return query.First(x => x.Name == code);
    }
}