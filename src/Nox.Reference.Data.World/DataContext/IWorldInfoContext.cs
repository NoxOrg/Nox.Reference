using Nox.Reference.Abstractions;
using Nox.Reference.Abstractions.Cultures;
using Nox.Reference.Abstractions.TimeZones;

namespace Nox.Reference.Data.World;

public interface IWorldInfoContext
{
    IQueryable<ICurrencyInfo> Currencies { get; }
    IQueryable<ICultureInfo> Cultures { get; }
    IQueryable<ITimeZoneInfo> TimeZones { get; }
    IQueryable<ILanguageInfo> Languages { get; }
    IQueryable<IVatNumberDefinitionInfo> VatNumberDefinitions { get; }
    IQueryable<ICountryHolidayInfo> Holidays { get; }
    IQueryable<ICountryInfo> Countries { get; }
}