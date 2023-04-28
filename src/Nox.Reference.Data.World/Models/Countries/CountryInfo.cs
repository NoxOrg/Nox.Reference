using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

internal class CountryInfo : ICountryInfo
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    [JsonPropertyName("code")] public string Code { get; set; } = string.Empty;
    [JsonPropertyName("languages")] public IReadOnlyList<string> Languages { get; set; } = Array.Empty<string>();
    [JsonPropertyName("names")] public ICountryNames Names { get; set; } = null!;
    [JsonPropertyName("topLevelDomains")] public IReadOnlyList<string> TopLevelDomains { get; set; } = Array.Empty<string>();
    [JsonPropertyName("alphaCode2")] public string AlphaCode2 { get; set; } = string.Empty;
    [JsonPropertyName("numericCode")] public string NumericCode { get; set; } = string.Empty;
    [JsonPropertyName("alphaCode3")] public string AlphaCode3 { get; set; } = string.Empty;
    [JsonPropertyName("olympicCommitteeCode")] public string OlympicCommitteeCode { get; set; } = string.Empty;
    [JsonPropertyName("fifaCode")] public string FifaCode { get; set; } = string.Empty;
    [JsonPropertyName("fipsCode")] public string FipsCode { get; set; } = string.Empty;
    [JsonPropertyName("isIndependent")] public bool? IsIndependent { get; set; }
    [JsonPropertyName("codeAssignedStatus")] public string CodeAssignedStatus { get; set; } = string.Empty;
    [JsonPropertyName("isUnitedNationsMember")] public bool IsUnitedNationsMember { get; set; }
    [JsonPropertyName("currencies")] public IReadOnlyList<string> Currencies { get; set; } = Array.Empty<string>();
    [JsonPropertyName("dialingInfo")] public IDialingInfo DialingInfo { get; set; } = null!;
    [JsonPropertyName("capitals")] public IReadOnlyList<string> Capitals { get; set; } = Array.Empty<string>();
    [JsonPropertyName("capitalInfo")] public ICapitalInfo CapitalInfo { get; set; } = null!;
    [JsonPropertyName("alternateSpellings")] public IReadOnlyList<string> AlternateSpellings { get; set; } = Array.Empty<string>();
    [JsonPropertyName("region")] public string Region { get; set; } = string.Empty;
    [JsonPropertyName("subRegion")] public string SubRegion { get; set; } = string.Empty;
    [JsonPropertyName("continents")] public IReadOnlyList<string> Continents { get; set; } = Array.Empty<string>();
    [JsonPropertyName("nameTranslations")] public IReadOnlyList<INativeNameInfo> NameTranslations { get; set; } = Array.Empty<NativeNameInfo>();
    [JsonPropertyName("geoCoordinates")] public IGeoCoordinates GeoCoordinates { get; set; } = new GeoCoordinatesInfo();
    [JsonPropertyName("isLandlocked")] public bool IsLandlocked { get; set; }
    [JsonPropertyName("borderingCountries")] public IReadOnlyList<string> BorderingCountries { get; set; } = Array.Empty<string>();
    [JsonPropertyName("landAreaInSquareKilometers")] public decimal LandAreaInSquareKilometers { get; set; }
    [JsonPropertyName("emojiFlag")] public string EmojiFlag { get; set; } = string.Empty;
    [JsonPropertyName("demonyms")] public IReadOnlyList<IDemonymn> Demonyms { get; set; } = Array.Empty<DemonymnInfo>();
    [JsonPropertyName("flags")] public IFlags Flags { get; set; } = null!;
    [JsonPropertyName("coatOfArms")] public ICoatOfArms CoatOfArms { get; set; } = null!;
    [JsonPropertyName("population")] public decimal Population { get; set; }
    [JsonPropertyName("maps")] public IMaps Maps { get; set; } = null!;
    [JsonPropertyName("giniCoefficients")] public IReadOnlyDictionary<string, decimal>? GiniCoefficients { get; set; }
    [JsonPropertyName("vehicleInfo")] public IVehicleInfo VehicleInfo { get; set; } = null!;
    [JsonPropertyName("postalCodeInfo")] public IPostalCodeInfo PostalCodeInfo { get; set; } = null!;
    [JsonPropertyName("startOfWeek")] public string StartOfWeek { get; set; } = string.Empty;
    [JsonPropertyName("startDayOfWeek")] public DayOfWeek StartDayOfWeek { get; set; }
    [JsonPropertyName("timeZones")] public IReadOnlyList<string> TimeZones { get; set; } = Array.Empty<string>();
}