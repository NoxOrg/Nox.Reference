using Microsoft.EntityFrameworkCore;

namespace Nox.Reference.Data.World.Extensions.Queries;

public static class CountryQueryExtensions
{
    public static Country? Get(this IQueryable<Country> query, string countryCode)
    {
        return query.FirstOrDefault(x => x.Code == countryCode);
    }

    public static CountryNameTranslation? GetTranslation(this Country info, string languageCode)
    {
        return info.NameTranslations!.FirstOrDefault(x => x.Language.Iso_639_3 == languageCode);
    }
}