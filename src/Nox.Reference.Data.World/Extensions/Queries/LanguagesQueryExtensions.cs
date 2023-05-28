namespace Nox.Reference.Data.World.Extensions.Queries;

public static class LanguagesQueryExtensions
{
    public static Language? Get(this IQueryable<Language> query, string isoCode)
    {
        return query.GetByIso_639_1(isoCode);
    }

    public static Language? GetByEnglishName(this IQueryable<Language> query, string isoCode)
    {
        return query.FirstOrDefault(x => x.Name == isoCode);
    }

    public static Language? GetByIso_639_1(this IQueryable<Language> query, string isoCode)
    {
        return query.FirstOrDefault(x => x.Iso_639_1 == isoCode);
    }

    public static Language? GetByIso_639_2b(this IQueryable<Language> query, string isoCode)
    {
        return query.FirstOrDefault(x => x.Iso_639_2b == isoCode);
    }

    public static Language? GetByIso_639_2t(this IQueryable<Language> query, string isoCode)
    {
        return query.FirstOrDefault(x => x.Iso_639_2t == isoCode);
    }

    public static Language? GetByIso_639_3(this IQueryable<Language> query, string isoCode)
    {
        return query.FirstOrDefault(x => x.Iso_639_3 == isoCode);
    }
}