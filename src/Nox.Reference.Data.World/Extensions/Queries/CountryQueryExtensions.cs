using Nox.Reference.Common;
using Nox.Reference.Common.Utilities;
using Nox.Reference.World;

namespace Nox.Reference.Data.World.Extensions.Queries;

public static class CountryQueryExtensions
{
    /// <summary>
    /// This method returns country info by alpha 2 code
    /// <example>
    /// <code>
    /// Countries.Get("UA")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="countryCode">Country alpha 2 code. Example: "UA".</param>
    /// <returns>Country info</returns>
    public static Country? Get(this IQueryable<Country> query, string countryCode)
    {
        return query.GetByAlpha2Code(countryCode);
    }

    /// <summary>
    /// This method returns country info by alpha 3 code
    /// <example>
    /// <code>
    /// Countries.GetByAlpha3Code("UKR")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="countryCode">Country alpha 3 code. Example: "UKR".</param>
    /// <returns>Country info</returns>
    public static Country? GetByAlpha3Code(this IQueryable<Country> query, string countryCode)
    {
        return query.FirstOrDefault(x => x.AlphaCode3 == countryCode.ToUpper());
    }

    /// <summary>
    /// This method returns country info by alpha 2 code
    /// <example>
    /// <code>
    /// Countries.GetByAlpha2Code("UA")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="countryCode">Country alpha 2 code. Example: "UA".</param>
    /// <returns>Country info</returns>
    public static Country? GetByAlpha2Code(this IQueryable<Country> query, string countryCode)
    {
        return query.FirstOrDefault(x => x.AlphaCode2 == countryCode.ToUpper());
    }

    /// <summary>
    /// This method returns country info by numeric code
    /// <example>
    /// <code>
    /// Countries.GetByNumericCode("804")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="countryCode">Country numeric code. Example: "804".</param>
    /// <returns>Country info</returns>
    public static Country? GetByNumericCode(this IQueryable<Country> query, string countryCode)
    {
        return query.FirstOrDefault(x => x.NumericCode == countryCode);
    }

    /// <summary>
    /// This method returns country info by Olympic committee code
    /// <example>
    /// <code>
    /// Countries.GetByOlympicCommitteeCode("UKR")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="countryCode">Country Olympic committee code. Example: "UKR".</param>
    /// <returns>Country info</returns>
    public static Country? GetByOlympicCommitteeCode(this IQueryable<Country> query, string countryCode)
    {
        return query.FirstOrDefault(x => x.OlympicCommitteeCode == countryCode.ToUpper());
    }

    /// <summary>
    /// This method returns country info by fifa code
    /// <example>
    /// <code>
    /// Countries.GetByFifaCode("UKR")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="countryCode">Country fifa code. Example: "UKR".</param>
    /// <returns>Country info</returns>
    public static Country? GetByFifaCode(this IQueryable<Country> query, string countryCode)
    {
        return query.FirstOrDefault(x => x.FifaCode == countryCode.ToUpper());
    }

    /// <summary>
    /// This method returns country info by fips code
    /// <example>
    /// <code>
    /// Countries.GetByFipsCode("UA")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="countryCode">Country fips code. Example: "UP".</param>
    /// <returns>Country info</returns>
    public static Country? GetByFipsCode(this IQueryable<Country> query, string countryCode)
    {
        return query.FirstOrDefault(x => x.FipsCode == countryCode.ToUpper());
    }

    /// <summary>
    /// This method returns country info by common name
    /// <example>
    /// <code>
    /// Countries.GetByCommonEnglishName("United States")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="commonName">Country common name. Example: "United States".</param>
    /// <returns>Country info</returns>
    public static Country? GetByCommonEnglishName(this IQueryable<Country> query, string commonName)
    {
        return query.FirstOrDefault(x => x.Names.CommonName.ToUpper() == commonName.ToUpper());
    }

    // TODO: add test for utf-32 (for example arabic) characters
    /// <summary>
    /// This method returns country info by official name
    /// <example>
    /// <code>
    /// Countries.GetByOfficialEnglishName("United States of America")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="officialName">Country official name. Example: "United States of America".</param>
    /// <returns>Country info</returns>
    public static Country? GetByOfficialEnglishName(this IQueryable<Country> query, string officialName)
    {
        return query.FirstOrDefault(x => x.Names.OfficialName.ToUpper() == officialName.ToUpper());
    }

    /// <summary>
    /// This method returns country translation in language by iso 639 1 code if exists
    /// <example>
    /// <code>
    /// Countries.GetTranslation("br")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="info">Current country</param>
    /// <param name="languageCode">Language iso 639 1 code. Example: "br".</param>
    /// <returns>Name translation or null</returns>
    public static CountryNameTranslation? GetTranslation(this Country info, string languageCode)
    {
        return info.NameTranslations!.FirstOrDefault(x => x.Language.Iso_639_1 == languageCode.ToLower());
    }

    /// <summary>
    /// This method returns country translation in language by currency enum
    /// <example>
    /// <code>
    /// Countries.Get(WorldCountries.Austria)
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="country">Country enum value. Example: WorldCountries.Austria.</param>
    /// <returns>Name translation or null</returns>
    public static Country? Get(this IQueryable<Country> query, WorldCountries country)
    {
        return query.FirstOrDefault(x => x.Name == EnumHelper.GetItemDescription(country));
    }
}