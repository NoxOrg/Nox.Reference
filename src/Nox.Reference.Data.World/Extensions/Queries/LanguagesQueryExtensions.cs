namespace Nox.Reference.Data.World.Extensions.Queries;

public static class LanguagesQueryExtensions
{
    public static Language? Get(this IQueryable<Language> query, string isoCode)
    {
        return query.FirstOrDefault(x => x.Iso_639_1 == isoCode);
    }
}