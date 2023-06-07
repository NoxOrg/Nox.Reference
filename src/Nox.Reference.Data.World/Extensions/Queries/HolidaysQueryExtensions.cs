using Nox.Reference.Common;
using Nox.Reference.World;

namespace Nox.Reference.Data.World.Extensions.Queries;

public static class HolidaysQueryExtensions
{
    /// <summary>
    /// Gets holiday info
    /// <example>
    /// <code>
    /// Holidays.Get(2023, "UA")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="year">Year to get holiday info. Example: 2023.</param>
    /// <param name="countryCode">Country to get holiday info alpha code. Example: "UA".</param>
    /// <returns>Holiday info per country</returns>
    public static CountryHoliday? Get(
        this IQueryable<CountryHoliday> query,
        int year,
        string countryCode)
    {
        return query.FirstOrDefault(x => x.Year == year && x.Country.AlphaCode2 == countryCode.ToUpper());
    }

    /// <summary>
    /// Gets holiday info
    /// <example>
    /// <code>
    /// Holidays.Get(2023, WorldCountries.Austria)
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="year">Year to get holiday info. Example: 2023.</param>
    /// <param name="country">Country to get holiday info enum value. Example: WorldCountries.Austria.</param>
    /// <returns>Holiday info per country</returns>
    public static CountryHoliday? Get(
        this IQueryable<CountryHoliday> query,
        int year,
        WorldCountries country)
    {
        return query.FirstOrDefault(x => x.Year == year && x.Country.Name == country.ToString());
    }
}