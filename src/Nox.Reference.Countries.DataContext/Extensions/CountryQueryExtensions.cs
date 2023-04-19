using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.Country.DataContext;

public static class CountryQueryExtensions
{
    public static ICurrencyInfo? GetByIsoCode(this IQueryable<ICurrencyInfo> query, string isoCode)
    {
        return query.FirstOrDefault(x => x.IsoCode == isoCode);
    }

    public static ICurrencyInfo? GetByIsoNumber(this IQueryable<ICurrencyInfo> query, string isoNumber)
    {
        return query.FirstOrDefault(x => x.IsoNumber == isoNumber);
    }
}