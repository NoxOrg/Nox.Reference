using System.Reflection.Metadata;

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
    /// <param name="year">Year to get holiday info. Example: 2023.</param>
    /// <param name="countryCode">Country to get holiday info alpha code. Example: "UA".</param>
    /// <returns>Holiday info per country</returns>
    public static CountryHoliday? Get(
        this IQueryable<CountryHoliday> query,
        int year,
        string countryCode)
    {
        return query.FirstOrDefault(x => x.Year == year && x.Country.Equals(countryCode, StringComparison.OrdinalIgnoreCase));
    }
}