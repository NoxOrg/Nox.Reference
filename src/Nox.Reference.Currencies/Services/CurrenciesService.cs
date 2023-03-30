using Nox.Reference.Abstractions.Currencies;
using Nox.Reference.Common;
using System.Reflection;

namespace Nox.Reference.Countries;

public class CurrenciesService : ICurrenciesService
{
    private readonly IReadOnlyList<ICurrencyInfo> _currencies;
    private readonly Dictionary<string, ICurrencyInfo> _currenciesByIsoCode = new Dictionary<string, ICurrencyInfo>();
    private readonly Dictionary<string, ICurrencyInfo> _currenciesByIsoNumber = new Dictionary<string, ICurrencyInfo>();

    public CurrenciesService()
    {
        var resourceName = "Nox.Reference.Currencies.json";

        _currencies = new List<CurrencyInfo>(AssemblyDataInitializer.GetDataFromAssemblyResource<CurrencyInfo>(resourceName));

        foreach (var currency in _currencies)
        {
            _currenciesByIsoCode[currency.IsoCode] = currency;

            if (!string.IsNullOrWhiteSpace(currency.IsoNumber))
            {
                _currenciesByIsoNumber[currency.IsoNumber] = currency;
            }
        }
    }

    public IReadOnlyList<ICurrencyInfo> GetCurrencies() => _currencies;

    public ICurrencyInfo? GetCurrencyByIsoCode(string isoCode)
    {
        _currenciesByIsoCode.TryGetValue(isoCode, out ICurrencyInfo? currency);
        return currency;
    }

    public ICurrencyInfo? GetCurrencyByIsoNumber(string isoNumber)
    {
        _currenciesByIsoNumber.TryGetValue(isoNumber, out ICurrencyInfo? currency);
        return currency;
    }
}