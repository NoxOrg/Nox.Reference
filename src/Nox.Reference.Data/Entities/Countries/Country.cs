namespace Nox.Reference.Data;

internal class Country : LocalizableEntityBase<CountryLocalization>, INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public IReadOnlyList<Language> Languages { get; set; }
    public CountryLocalization Localization { get; private set; }
    public IReadOnlyList<TopLevelDomain> TopLevelDomains { get; set; }
    public string AlphaCode2 { get; set; }
    public string NumericCode { get; set; }
    public string AlphaCode3 { get; set; }
    public string OlympicCommitteeCode { get; set; }
    public string FifaCode { get; set; }
    public string FipsCode { get; set; }
    public bool? IsIndependent { get; set; }
    public string CodeAssignedStatus { get; set; }
    public bool IsUnitedNationsMember { get; set; }
    public IReadOnlyList<Currency>? Currencies { get; set; }
    public Dialing? Dialing { get; set; }
    public IReadOnlyList<City> Capitals { get; set; }
    public City? Capital { get; set; }
    public string Region { get; set; }
    public string SubRegion { get; set; }
    public IReadOnlyList<string> Continents { get; set; }
    public GeoCoordinates GeoCoordinates { get; set; }
    public bool IsLandlocked { get; private set; }
    public IReadOnlyList<Country> BorderingCountries { get; set; }
    public decimal LandAreaInSquareKilometers { get; set; }
    public string EmojiFlag { get; set; }
    public IReadOnlyList<Demonymn>? Demonyms { get; set; }
    public Flags Flags { get; private set; }
    public CoatOfArms CoatOfArms { get; set; }
    public decimal Population { get; set; }
    public Maps Maps { get; private set; }
    public IReadOnlyList<GiniCoefficient> GiniCoefficients { get; set; }
    public Vehicle? Vehicle { get; set; }
    public string StartOfWeek { get; set; }
    public DayOfWeek StartDayOfWeek { get; private set; }
    public IReadOnlyList<TimeZoneInfo> TimeZones { get; set; }
}