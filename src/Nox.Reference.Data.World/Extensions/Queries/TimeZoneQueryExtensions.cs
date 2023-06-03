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
        return query.FirstOrDefault(x => x.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
    }
}