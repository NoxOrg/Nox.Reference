using Nox.Reference.Abstractions.Currencies;
using Nox.Reference.Data.Repositories;

namespace Nox.Reference.Currencies;

internal class CurrenciesService : ICurrenciesService
{
    private readonly INoxReferenceContext<ICurrencyInfo> _repository;

    public CurrenciesService(INoxReferenceContext<ICurrencyInfo> repository)
    {
        _repository = repository;
    }

    public IReadOnlyList<ICurrencyInfo> GetCurrencies()
    {
        return _repository.Set.ToList(); ;
    }

    public ICurrencyInfo? GetCurrencyByIsoCode(string isoCode)
    {
        return _repository.Set.FirstOrDefault(x => x.IsoCode == isoCode);
    }

    public ICurrencyInfo? GetCurrencyByIsoNumber(string isoNumber)
    {
        return _repository.Set.FirstOrDefault(x => x.IsoNumber == isoNumber);
    }
}