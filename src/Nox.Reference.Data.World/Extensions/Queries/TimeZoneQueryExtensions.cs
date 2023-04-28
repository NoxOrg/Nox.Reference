using Nox.Reference.Abstractions.TimeZones;

namespace Nox.Reference.Data.World.Extensions.Queries;

internal static class TimeZoneQueryExtensions
{
    public static ITimeZoneInfo? Get(this IQueryable<ITimeZoneInfo> query, string code)
    {
        return query.FirstOrDefault(x => x.Id == code);
    }
}