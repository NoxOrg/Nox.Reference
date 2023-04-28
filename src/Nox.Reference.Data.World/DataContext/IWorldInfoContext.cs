using Nox.Reference.Abstractions;
using Nox.Reference.Abstractions.Cultures;

namespace Nox.Reference.Data.World;

public interface IWorldInfoContext
{
    IQueryable<ICurrencyInfo> Currencies { get; }
    IQueryable<ILanguageInfo> Languages { get; }
    IQueryable<IVatNumberDefinitionInfo> VatNumberDefinitions { get; }
    IQueryable<ICountryHolidayInfo> Holidays { get; }
    IQueryable<ICultureInfo> Cultures { get; }
}