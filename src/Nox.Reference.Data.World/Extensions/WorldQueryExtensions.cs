using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.Data;

public static class WorldQueryExtensions
{
    public static ICurrencyInfo? Get(this IQueryable<ICurrencyInfo> query, string currency)
    {
        return query.FirstOrDefault(x => x.IsoCode == currency);
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