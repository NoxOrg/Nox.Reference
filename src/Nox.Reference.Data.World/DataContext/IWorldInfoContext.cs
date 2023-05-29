namespace Nox.Reference.Data.World;

public interface IWorldInfoContext
{
    IQueryable<Currency> Currencies { get; }
    IQueryable<Language> Languages { get; }
    IQueryable<VatNumberDefinition> VatNumberDefinitions { get; }
    IQueryable<CountryHoliday> Holidays { get; }
    IQueryable<Culture> Cultures { get; }
    IQueryable<TimeZone> TimeZones { get; }
    IQueryable<Country> Countries { get; }
    IQueryable<CarrierPhoneNumber> CarrierPhoneNumbers { get; }
    IQueryable<PhoneCarrier> PhoneCarriers { get; }
}