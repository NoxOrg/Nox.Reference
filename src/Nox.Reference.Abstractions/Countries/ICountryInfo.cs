namespace Nox.Reference.Abstractions;

public interface ICountryInfo
{
    public int Id { get; }
    public string Name { get; }
    public string Code { get; }
    public IReadOnlyList<string> Languages { get; }
    public ICountryNames Names { get; }
    public IReadOnlyList<string> TopLevelDomains { get; }
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
    public IReadOnlyList<string> Capitals { get; }
    public ICapitalInfo? CapitalInfo { get; }
    public IReadOnlyList<string> AlternateSpellings { get; }
    public string Region { get; }
    public string SubRegion { get; }
    public IReadOnlyList<string> Continents { get; }
    public IReadOnlyList<INativeNameInfo>? NameTranslations { get; }
    public IGeoCoordinates GeoCoordinates { get; }
    public bool IsLandlocked { get; }
    public IReadOnlyList<string> BorderingCountries { get; }
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
    public IReadOnlyList<string> TimeZones { get; }
}