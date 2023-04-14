using Nox.Reference.Abstractions.Currencies;
using Nox.Reference.Data.Repositories;

namespace Nox.Reference.Currencies;

internal class CurrenciesService : ICurrenciesService
{
    private readonly INoxReferenceContext<ICurrencyInfo> _currencyContext;

    public CurrenciesService(INoxReferenceContext<ICurrencyInfo> currencyContext)
    {
        _currencyContext = currencyContext;
    }

    public IReadOnlyList<ICurrencyInfo> GetCurrencies()
    {
        var p = _currencyContext.Query().Where(x => x.Banknotes.Frequent.Count() > 2).ToList();
        return p;
    }

    public ICurrencyInfo? GetCurrencyByIsoCode(string isoCode)
    {
        return _currencyContext.Query().FirstOrDefault(x => x.IsoCode == isoCode);
    }

    public ICurrencyInfo? GetCurrencyByIsoNumber(string isoNumber)
    {
        return _currencyContext.Query().FirstOrDefault(x => x.IsoNumber == isoNumber);
    }
}