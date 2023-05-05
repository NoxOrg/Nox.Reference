namespace Nox.Reference.Abstractions;

public interface ICountryInfo
{
    string Id { get; }
    string Name { get; }
    string Code { get; }
    IReadOnlyList<string> Languages { get; }
    ICountryNames Names { get; }
    IReadOnlyList<string> TopLevelDomains { get; }
    string AlphaCode2 { get; }
    string NumericCode { get; }
    string AlphaCode3 { get; }
    string OlympicCommitteeCode { get; }
    string FifaCode { get; }
    string FipsCode { get; }
    bool? IsIndependent { get; }
    string CodeAssignedStatus { get; }
    bool IsUnitedNationsMember { get; }
    IReadOnlyList<string>? Currencies { get; }
    IDialingInfo? DialingInfo { get; }
    IReadOnlyList<string> Capitals { get; }
    ICapitalInfo? CapitalInfo { get; }
    IReadOnlyList<string> AlternateSpellings { get; }
    string Region { get; }
    string SubRegion { get; }
    IReadOnlyList<string> Continents { get; }
    IReadOnlyList<INativeNameInfo>? NameTranslations { get; }
    IGeoCoordinates GeoCoordinates { get; }
    bool IsLandlocked { get; }
    IReadOnlyList<string> BorderingCountries { get; }
    decimal LandAreaInSquareKilometers { get; }
    string EmojiFlag { get; }
    IReadOnlyList<IDemonymn>? Demonyms { get; }
    IFlags Flags { get; }
    ICoatOfArms CoatOfArms { get; }
    decimal Population { get; }
    IMaps Maps { get; }
    IReadOnlyDictionary<int, decimal>? GiniCoefficients { get; }
    IVehicleInfo? VehicleInfo { get; }
    IPostalCodeInfo? PostalCodeInfo { get; }
    string StartOfWeek { get; }
    DayOfWeek StartDayOfWeek { get; }
    IReadOnlyList<string> TimeZones { get; }
}