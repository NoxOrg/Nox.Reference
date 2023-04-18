using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.Currencies
{
    public interface ICurrenciesService
    {
        /// <summary>
        /// Get currencies list
        /// </summary>
        /// <returns>IReadOnlyList with all supported currencies and their info</returns>
        IReadOnlyList<ICurrencyInfo> GetCurrencies();

        /// <summary>
        /// Get currency info by iso code
        /// </summary>
        /// <param name="countryIsoCode">Country iso 2 code</param>
        /// <returns>Currency info by iso code</returns>
        ICurrencyInfo? GetCurrencyByIsoCode(string countryIsoCode);

        /// <summary>
        /// Get currency info by full country name
        /// </summary>
        /// <param name="countryName">Country full name</param>
        /// <returns>Currency info by full country name</returns>
        ICurrencyInfo? GetCurrencyByIsoNumber(string countryName);
    }
}