using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.Currencies
{
    public interface ICurrenciesService
    {
        IReadOnlyList<ICurrencyInfo> GetCurrencies();

        ICurrencyInfo? GetCurrencyByIsoCode(string countryIsoCode);

        ICurrencyInfo? GetCurrencyByIsoNumber(string countryName);
    }
}