using Nox.Reference.Abstractions;
using Nox.Reference.Abstractions.Cultures;
using Nox.Reference.Abstractions.TimeZones;

namespace Nox.Reference.Data;

public static class WorldQueryExtensions
{
    public static ICultureInfo? Get(this IQueryable<ICultureInfo> query, string code)
    {
        return query.First(x => x.Id == code);
    }

    public static ITimeZoneInfo? Get(this IQueryable<ITimeZoneInfo> query, string code)
    {
        return query.First(x => x.Id == code);
    }

    public static ICurrencyInfo Get(this IQueryable<ICurrencyInfo> query, string currency)
    {
        return query.First(x => x.IsoCode == currency);
    }

    public static ICurrencyInfo? GetByIsoCode(this IQueryable<ICurrencyInfo> query, string isoCode)
    {
        return query.FirstOrDefault(x => x.IsoCode == isoCode);
    }

    public static ICurrencyInfo? GetByIsoNumber(this IQueryable<ICurrencyInfo> query, string isoNumber)
    {
        return query.FirstOrDefault(x => x.IsoNumber == isoNumber);
    }
}