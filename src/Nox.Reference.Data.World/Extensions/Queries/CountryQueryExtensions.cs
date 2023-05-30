namespace Nox.Reference.Data.World.Extensions.Queries;

public static class CountryQueryExtensions
{
    public static Country? Get(this IQueryable<Country> query, string countryCode)
    {
        return query.GetByAlpha3Code(countryCode);
    }

    public static Country? GetByAlpha3Code(this IQueryable<Country> query, string countryCode)
    {
        return query.FirstOrDefault(x => x.Code == countryCode);
    }

    public static Country? GetByAlpha2Code(this IQueryable<Country> query, string countryCode)
    {
        return query.FirstOrDefault(x => x.AlphaCode2 == countryCode);
    }

    public static Country? GetByNumericCode(this IQueryable<Country> query, string countryCode)
    {
        return query.FirstOrDefault(x => x.NumericCode == countryCode);
    }

    public static Country? GetByOlympicCommitteeCode(this IQueryable<Country> query, string countryCode)
    {
        return query.FirstOrDefault(x => x.OlympicCommitteeCode == countryCode);
    }

    public static Country? GetByFifaCode(this IQueryable<Country> query, string countryCode)
    {
        return query.FirstOrDefault(x => x.FifaCode == countryCode);
    }

    public static Country? GetByFipsCode(this IQueryable<Country> query, string countryCode)
    {
        return query.FirstOrDefault(x => x.FipsCode == countryCode);
    }

    public static Country? GetByCommonEnglishName(this IQueryable<Country> query, string countryCode)
    {
        return query.FirstOrDefault(x => x.Names.CommonName == countryCode);
    }

    public static Country? GetByOfficialEnglishName(this IQueryable<Country> query, string countryCode)
    {
        return query.FirstOrDefault(x => x.Names.OfficialName == countryCode);
    }

    public static CountryNameTranslation? GetTranslation(this Country info, string languageCode)
    {
        return info.NameTranslations!.FirstOrDefault(x => x.Language.Iso_639_1 == languageCode);
    }
}