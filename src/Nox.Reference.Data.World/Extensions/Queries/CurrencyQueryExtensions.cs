namespace Nox.Reference.Data.World.Extensions.Queries;

public static class CurrencyQueryExtensions
{
    public static Currency? Get(this IQueryable<Currency> query, string currency)
    {
        return query.GetByIsoCode(currency);
    }

    public static Currency? GetByIsoCode(this IQueryable<Currency> query, string isoCode)
    {
        return query.FirstOrDefault(x => x.IsoCode == isoCode);
    }

    public static Currency? GetByIsoNumber(this IQueryable<Currency> query, string isoNumber)
    {
        return query.FirstOrDefault(x => x.IsoNumber == isoNumber);
    }
}