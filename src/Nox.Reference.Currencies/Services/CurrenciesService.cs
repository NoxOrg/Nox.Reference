using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.Currencies;

internal class CurrenciesService : ICurrenciesService
{
    private static IReadOnlyList<ICurrencyInfo> _currencies = new List<ICurrencyInfo>();
    private static readonly Dictionary<string, ICurrencyInfo> _currenciesByIsoCode = new Dictionary<string, ICurrencyInfo>();
    private static readonly Dictionary<string, ICurrencyInfo> _currenciesByIsoNumber = new Dictionary<string, ICurrencyInfo>();

    public static void Init(IEnumerable<ICurrencyInfo> currencies)
    {
        _currencies = new List<ICurrencyInfo>(currencies);

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