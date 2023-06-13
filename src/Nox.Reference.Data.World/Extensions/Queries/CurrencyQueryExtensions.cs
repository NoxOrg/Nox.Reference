using Nox.Reference.Common;
using Nox.Reference.Common.Utilities;
using Nox.Reference.World;

namespace Nox.Reference.Data.World.Extensions.Queries;

public static class CurrencyQueryExtensions
{
    /// <summary>
    /// This method returns currency info by currency ISO Code
    /// <example>
    /// <code>
    /// Currencies.Get("AED")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="currencyIsoCode">Currency ISO Code. Example: "AED".</param>
    /// <returns>Currency info</returns>
    public static Currency? Get(this IQueryable<Currency> query, string currencyIsoCode)
    {
        return query.GetByIsoCode(currencyIsoCode);
    }

    /// <summary>
    /// This method returns currency info by currency ISO Code
    /// <example>
    /// <code>
    /// Currencies.GetByIsoCode("AED")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="isoCode">Currency ISO Code Example: 'AED'</param>
    /// <returns>Currency info</returns>
    public static Currency? GetByIsoCode(this IQueryable<Currency> query, string isoCode)
    {
        return query.FirstOrDefault(x => x.IsoCode == isoCode.ToUpper());
    }

    /// <summary>
    /// This method returns currency info by currency ISO Number
    /// <example>
    /// <code>
    /// Currencies.GetByIsoNumber("784")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="isoNumber">Currency ISO Number. Example: '784'</param>
    /// <returns>Currency info</returns>
    public static Currency? GetByIsoNumber(this IQueryable<Currency> query, string isoNumber)
    {
        return query.FirstOrDefault(x => x.IsoNumber == isoNumber);
    }

    /// <summary>
    /// This method returns currency info by currency enum value
    /// <example>
    /// <code>
    /// Currencies.Get(WorldCurrencies.DutchGuilder)
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="currency">Currency enum value. Example: WorldCurrencies.DutchGuilder.</param>
    /// <returns>Currency info</returns>
    public static Currency? Get(this IQueryable<Currency> query, WorldCurrencies currency)
    {
        return query.FirstOrDefault(x => x.Name == EnumHelper.GetItemDescription(currency));
    }
}