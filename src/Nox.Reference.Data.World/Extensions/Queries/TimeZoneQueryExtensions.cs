namespace Nox.Reference.Data.World.Extensions.Queries;

public static class TimeZoneQueryExtensions
{
    public static TimeZone? Get(this IQueryable<TimeZone> query, string code)
    {
        return query.GetById(code);
    }

    public static TimeZone? GetById(this IQueryable<TimeZone> query, string code)
    {
        return query.FirstOrDefault(x => x.Code == code);
    }
}