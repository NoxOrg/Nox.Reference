using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.Country.DataContext;

public interface ICountryContext
{
    IQueryable<ICurrencyInfo> Currencies { get; }
}