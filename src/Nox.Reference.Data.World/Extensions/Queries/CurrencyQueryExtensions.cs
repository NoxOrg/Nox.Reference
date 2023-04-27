using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World.Extensions.Queries;

public static class CurrencyQueryExtensions
{
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