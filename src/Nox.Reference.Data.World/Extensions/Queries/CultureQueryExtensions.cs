using Nox.Reference.Common;
using Nox.Reference.World;

namespace Nox.Reference.Data.World.Extensions.Queries;

public static class CultureQueryExtensions
{
    /// <summary>
    /// This method returns culture info by culture name
    /// <example>
    /// <code>
    /// Cultures.Get("uz-Arab")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="code">Culture name. Example: "uz-Arab".</param>
    /// <returns>Culture info</returns>
    public static Culture? Get(this IQueryable<Culture> query, string code)
    {
        return query.GetByName(code);
    }

    /// <summary>
    /// This method returns culture info by culture name
    /// <example>
    /// <code>
    /// Cultures.GetByName("uz-Arab")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="code">Culture name. Example: "uz-Arab".</param>
    /// <returns>Culture info</returns>
    public static Culture? GetByName(this IQueryable<Culture> query, string code)
    {
        return query.FirstOrDefault(x => x.Name.ToUpper() == code.ToUpper());
    }

    /// <summary>
    /// This method returns culture info by culture formal name
    /// <example>
    /// <code>
    /// Cultures.GetByFormalName("Uzbek (Arabic)")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="formalName">Culture name. Example: "Uzbek (Arabic)".</param>
    /// <returns>Culture info</returns>
    public static Culture? GetByFormalName(this IQueryable<Culture> query, string formalName)
    {
        return query.FirstOrDefault(x => x.FormalName.ToUpper() == formalName.ToUpper());
    }

    /// <summary>
    /// This method returns culture info by culture native name
    /// <example>
    /// <code>
    /// Cultures.GetByNativeName("(اوزبیک(عربی")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="nativeName">Culture native name. Example: "(اوزبیک(عربی".</param>
    /// <returns>Culture info</returns>
    public static Culture? GetByNativeName(this IQueryable<Culture> query, string nativeName)
    {
        return query.FirstOrDefault(x => x.NativeName == nativeName);
    }

    /// <summary>
    /// This method returns culture info by country
    /// <example>
    /// <code>
    /// Cultures.GetByCountry(WorldCountries.UnitedStates)
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="country">Country enum. Example: WorldCountries.UnitedStates.</param>
    /// <returns>Culture info</returns>
    public static List<Culture> GetByCountry(this IQueryable<Culture> query, WorldCountries country)
    {
        return query.Where(x => x.Country != null && x.Country.Name == country.GetStringValue()).ToList();
    }
}