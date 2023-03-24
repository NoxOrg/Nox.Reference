using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.Countries
{
    public interface ICurrenciesService
    {
        IReadOnlyList<ICurrencyInfo> GetCurrencies();
        ICurrencyInfo? GetCurrencyByIsoCode(string countryIsoCode);
        ICurrencyInfo? GetCurrencyByIsoNumber(string countryName);
    }
}