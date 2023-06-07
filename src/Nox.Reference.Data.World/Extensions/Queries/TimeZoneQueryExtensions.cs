using Nox.Reference.Common;
using Nox.Reference.World;

namespace Nox.Reference.Data.World.Extensions.Queries;

public static class TimeZoneQueryExtensions
{
    /// <summary>
    /// Get time zone information by time zone id
    /// <example>
    /// <code>
    /// TimeZones.Get("Europe/Vilnius")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="code">Time zone code. Example: "Europe/Vilnius".</param>
    /// <returns>Time zone information</returns>
    public static TimeZone? Get(this IQueryable<TimeZone> query, string code)
    {
        return query.GetById(code);
    }

    /// <summary>
    /// Get time zone information by time zone id
    /// <example>
    /// <code>
    /// TimeZones.GetById("Europe/Vilnius")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="code">Time zone code. Example: "Europe/Vilnius".</param>
    /// <returns>Time zone information</returns>
    public static TimeZone? GetById(this IQueryable<TimeZone> query, string code)
    {
        return query.FirstOrDefault(x => x.Code.ToUpper() == code.ToUpper());
    }

    /// <summary>
    /// Get time zone information for country
    /// <example>
    /// <code>
    /// TimeZones.GetByCountry(WorldCountries.UnitedStates)
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="country">Country enum. Example: WorldCountries.UnitedStates.</param>
    /// <returns>Time zone information</returns>
    public static List<TimeZone>? GetByCountry(this IQueryable<TimeZone> query, WorldCountries country)
    {
        return query.Where(x => x.Countries.Any(x => x.Name == country.GetStringValue())).ToList();
    }
}