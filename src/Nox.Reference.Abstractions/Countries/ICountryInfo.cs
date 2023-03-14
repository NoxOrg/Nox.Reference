namespace Nox.Reference.Countries;
public interface ICountryInfo
{
    public int Id { get; }
    public string Name { get; }
    public string Code { get; }
    public IReadOnlyList<string> Languages { get; }
    public ICountryNames Names { get; }
    public string[] TopLevelDomains { get; }
    public string AlphaCode2 { get; }
    public string NumericCode { get; }
    public string AlphaCode3 { get; }
    public string OlympicCommitteeCode { get; }
    public string FifaCode { get; }
    public string FipsCode { get; }
    public bool? IsIndependent { get; }
    public string CodeAssignedStatus { get; }
    public bool IsUnitedNationsMember { get; }
    public IReadOnlyList<string>? Currencies { get; }
    public IDialingInfo? DialingInfo { get; }
    public string[] Capitals { get; }
    public ICapitalInfo? CapitalInfo { get; }
    public string[] AlternateSpellings { get; }
    public string Region { get; }
    public string SubRegion { get; }
    public string[] Continents { get; }
    public IReadOnlyList<INativeNameInfo>? NameTranslations { get; }
    public decimal[] LatLong { get; }
    public bool IsLandlocked { get; }
    public string[] BorderingCountries { get; }
    public decimal LandAreaInSquareKilometers { get; }
    public string EmojiFlag { get; }
    public IReadOnlyList<IDemonymn>? Demonyms { get; }
    public IFlags Flags { get; }
    public ICoatOfArms CoatOfArms { get; }
    public decimal Population { get; }
    public IMaps Maps { get; }
    public IReadOnlyDictionary<string, decimal>? GiniCoefficients { get; }
    public IVehicleInfo? VehicleInfo { get; }
    public IPostalCodeInfo? PostalCodeInfo { get; }
    public string StartOfWeek { get; }
    public DayOfWeek StartDayOfWeek { get; }
    public string[] TimeZones { get; }
}

