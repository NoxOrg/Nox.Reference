
using Nox.Reference.Abstractions.Currencies;
using System.Reflection;
using System.Text.Json;

namespace Nox.Reference.Countries;

public class CurrenciesService : ICurrenciesService
{
    private readonly IReadOnlyList<ICurrencyInfo> _currencies = new List<ICurrencyInfo>();
    private readonly Dictionary<string, ICurrencyInfo> _currenciesByIsoCode = new Dictionary<string, ICurrencyInfo>();
    private readonly Dictionary<string, ICurrencyInfo> _currenciesByIsoNumber = new Dictionary<string, ICurrencyInfo>();

    public CurrenciesService()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "Nox.Reference.Currencies.json";
        if (assembly == null)
        {
            return;
        }

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            return;
        }

        using var reader = new StreamReader(stream);

        _currencies = JsonSerializer.Deserialize<List<CurrencyInfo>>(
            reader.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) ?? new List<CurrencyInfo>();

        foreach (var currency in _currencies)
        {
            _currenciesByIsoCode[currency.IsoCode] = currency;
            _currenciesByIsoNumber[currency.IsoNumber] = currency;
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
