using Nox.Reference.Abstractions.TimeZones;

namespace Nox.Reference.Data.World.Extensions.Queries;

public static class TimeZoneQueryExtensions
{
    public static ITimeZoneInfo? Get(this IQueryable<ITimeZoneInfo> query, string code)
    {
        return query.FirstOrDefault(x => x.Id == code);
    }
}