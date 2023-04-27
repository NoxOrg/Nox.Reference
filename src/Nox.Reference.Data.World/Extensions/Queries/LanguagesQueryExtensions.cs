using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World.Extensions.Queries;

public static class LanguagesQueryExtensions
{
    public static ILanguageInfo? Get(this IQueryable<ILanguageInfo> query, string isoCode)
    {
        return query.FirstOrDefault(x => x.Iso_639_1 == isoCode);
    }
}