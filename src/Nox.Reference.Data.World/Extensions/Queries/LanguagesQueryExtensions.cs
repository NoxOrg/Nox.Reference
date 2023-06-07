using Nox.Reference.Common;
using Nox.Reference.World;

namespace Nox.Reference.Data.World.Extensions.Queries;

public static class LanguagesQueryExtensions
{
    /// <summary>
    /// Get language info by iso 639 1 code
    /// <example>
    /// <code>
    /// Languages.Get("fr")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="isoCode">Iso 639 1 code. Example: 'fr'.</param>
    /// <returns>Language info</returns>
    public static Language? Get(this IQueryable<Language> query, string isoCode)
    {
        return query.GetByIso_639_1(isoCode);
    }

    /// <summary>
    /// Get language info by English name
    /// <example>
    /// <code>
    /// Languages.GetByEnglishName("French")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="englishName">Language English name. Example: 'French'.</param>
    /// <returns>Language info</returns>
    public static Language? GetByEnglishName(this IQueryable<Language> query, string englishName)
    {
        return query.FirstOrDefault(x => x.Name.ToLower() == englishName.ToLower());
    }

    /// <summary>
    /// Get language info by iso 639 1 code
    /// <example>
    /// <code>
    /// Languages.GetByIso_639_1("fr")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="isoCode">Iso 639 1 code. Example: 'fr'.</param>
    /// <returns>Language info</returns>
    public static Language? GetByIso_639_1(this IQueryable<Language> query, string isoCode)
    {
        return query.FirstOrDefault(x => x.Iso_639_1 == isoCode.ToLower());
    }

    /// <summary>
    /// Get language info by iso 639 2b code
    /// <example>
    /// <code>
    /// Languages.GetByIso_639_2b("fre")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="isoCode">Iso 639 2b code. Example: 'fre'.</param>
    /// <returns>Language info</returns>
    public static Language? GetByIso_639_2b(this IQueryable<Language> query, string isoCode)
    {
        return query.FirstOrDefault(x => x.Iso_639_2b == isoCode.ToLower());
    }

    /// <summary>
    /// Get language info by iso 639 2t code
    /// <example>
    /// <code>
    /// Languages.GetByIso_639_2t("fra")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="isoCode">Iso 639 2t code. Example: 'fra'.</param>
    /// <returns>Language info</returns>
    public static Language? GetByIso_639_2t(this IQueryable<Language> query, string isoCode)
    {
        return query.FirstOrDefault(x => x.Iso_639_2t == isoCode.ToLower());
    }

    /// <summary>
    /// Get language info by iso 639 3 code
    /// <example>
    /// <code>
    /// Languages.GetByIso_639_3("fra")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="isoCode">Iso 639 3 code. Example: 'fra'.</param>
    /// <returns>Language info</returns>
    public static Language? GetByIso_639_3(this IQueryable<Language> query, string isoCode)
    {
        return query.FirstOrDefault(x => x.Iso_639_3 == isoCode.ToLower());
    }

    /// <summary>
    /// Get languages used in country
    /// <example>
    /// <code>
    /// Languages.GetLanguagesForCountry(WorldCountries.Austria)
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="country">Country enum. Example: WorldCountries.Austria.</param>
    /// <returns>Language info</returns>
    public static List<Language>? GetLanguagesForCountry(this IQueryable<Language> query, WorldCountries country)
    {
        return query.Where(x => x.Countries.Any(x => x.Name == country.GetStringValue())).ToList();
    }
}