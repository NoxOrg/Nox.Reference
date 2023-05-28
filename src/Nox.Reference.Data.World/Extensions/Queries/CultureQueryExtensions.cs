namespace Nox.Reference.Data.World.Extensions.Queries;

public static class CultureQueryExtensions
{
    public static Culture? Get(this IQueryable<Culture> query, string code)
    {
        return query.GetByName(code);
    }

    public static Culture? GetByName(this IQueryable<Culture> query, string code)
    {
        return query.FirstOrDefault(x => x.Name == code);
    }

    public static Culture? GetByFormalName(this IQueryable<Culture> query, string formalName)
    {
        return query.FirstOrDefault(x => x.FormalName == formalName);
    }

    public static Culture? GetByNativeName(this IQueryable<Culture> query, string nativeName)
    {
        return query.FirstOrDefault(x => x.NativeName == nativeName);
    }
}