using Nox.Reference.Common;
using Nox.Reference.World;

namespace Nox.Reference.Data.World.Extensions.Queries;

public static class CurrencyQueryExtensions
{
    /// <summary>
    /// Get currency by ISO curency code. 
    /// </summary>
    /// <param name="query">IQueryable<Currency> query</param>
    /// <param name="currency">Currency ISO code</param>
    /// <returns>Currency</returns>
    public static Currency? Get(this IQueryable<Currency> query, string currency)
    {
        return query.GetByIsoCode(currency);
    }

    /// <summary>
    ///  Get currency by defined WorldCurrencies enumeration. 
    /// </summary>
    /// <param name="query">IQueryable<Currency> query</param>
    /// <param name="currency">WorldCurrencies enumeration</param>
    /// <returns></returns>
    public static Currency? Get(this IQueryable<Currency> query, WorldCurrencies currency)
    {
        return query.FirstOrDefault(x => x.Name == currency.GetStringValue());
    }

    /// <summary>
    /// Get currency by ISO curency code. 
    /// </summary>
    /// <param name="query">IQueryable<Currency> query</param>
    /// <param name="currency">Currency ISO code</param>
    /// <returns>Currency</returns>
    public static Currency? GetByIsoCode(this IQueryable<Currency> query, string isoCode)
    {
        return query.FirstOrDefault(x => x.IsoCode == isoCode);
    }

    /// <summary>
    /// Get currency by ISO number. 
    /// </summary>
    /// <param name="query">IQueryable<Currency> query</param>
    /// <param name="currency">Currency ISO number</param>
    /// <returns>Currency</returns>
    public static Currency? GetByIsoNumber(this IQueryable<Currency> query, string isoNumber)
    {
        return query.FirstOrDefault(x => x.IsoNumber == isoNumber);
    }
}