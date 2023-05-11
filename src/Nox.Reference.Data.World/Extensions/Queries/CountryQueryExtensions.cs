using Microsoft.EntityFrameworkCore;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World.Extensions.Queries;

public static class CountryQueryExtensions
{
    public static ICountryInfo? Get(this IQueryable<ICountryInfo> query, string countryCode)
    {
        return query.FirstOrDefault(x => x.Id == countryCode);
    }

    public static INativeNameInfo? GetTranslation(this ICountryInfo info, string languageCode)
    {
        return info.NameTranslations!.FirstOrDefault(x => x.Language == languageCode);
    }
}