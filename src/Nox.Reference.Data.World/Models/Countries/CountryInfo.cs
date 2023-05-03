using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;
using Nox.Reference.Abstractions.Shared;
using Nox.Reference.Common;

namespace Nox.Reference.Data.World;

internal class CountryInfo : ICountryInfo
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("languages")]
    public IReadOnlyList<string> Languages { get; set; } = Array.Empty<string>();

    [JsonPropertyName("names")]
    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<ICountryNames, CountryNamesInfo>))]
    public ICountryNames Names { get; set; } = new CountryNamesInfo();

    [JsonPropertyName("topLevelDomains")]
    public IReadOnlyList<string> TopLevelDomains { get; set; } = Array.Empty<string>();

    [JsonPropertyName("alphaCode2")]
    public string AlphaCode2 { get; set; } = string.Empty;

    [JsonPropertyName("numericCode")]
    public string NumericCode { get; set; } = string.Empty;

    [JsonPropertyName("alphaCode3")]
    public string AlphaCode3 { get; set; } = string.Empty;

    [JsonPropertyName("olympicCommitteeCode")]
    public string OlympicCommitteeCode { get; set; } = string.Empty;

    [JsonPropertyName("fifaCode")]
    public string FifaCode { get; set; } = string.Empty;

    [JsonPropertyName("fipsCode")]
    public string FipsCode { get; set; } = string.Empty;

    [JsonPropertyName("isIndependent")]
    public bool? IsIndependent { get; set; }

    [JsonPropertyName("codeAssignedStatus")]
    public string CodeAssignedStatus { get; set; } = string.Empty;

    [JsonPropertyName("isUnitedNationsMember")]
    public bool IsUnitedNationsMember { get; set; }

    [JsonPropertyName("currencies")]
    public IReadOnlyList<string> Currencies { get; set; } = Array.Empty<string>();

    [JsonPropertyName("dialingInfo")]
    public IDialingInfo DialingInfo { get; set; } = new DialingInfo();

    [JsonPropertyName("capitals")]
    public IReadOnlyList<string> Capitals { get; set; } = Array.Empty<string>();

    [JsonPropertyName("capitalInfo")]
    public ICapitalInfo CapitalInfo { get; set; } = new CapitalInfo();

    [JsonPropertyName("alternateSpellings")]
    public IReadOnlyList<string> AlternateSpellings { get; set; } = Array.Empty<string>();

    [JsonPropertyName("region")]
    public string Region { get; set; } = string.Empty;

    [JsonPropertyName("subRegion")]
    public string SubRegion { get; set; } = string.Empty;

    [JsonPropertyName("continents")]
    public IReadOnlyList<string> Continents { get; set; } = Array.Empty<string>();

    [JsonPropertyName("nameTranslations")]
    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<IReadOnlyList<INativeNameInfo>, NativeNameInfo[]>))]
    public IReadOnlyList<INativeNameInfo> NameTranslations { get; set; } = Array.Empty<NativeNameInfo>();

    [JsonPropertyName("geoCoordinates")]
    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<IGeoCoordinates, GeoCoordinatesInfo>))]
    public IGeoCoordinates GeoCoordinates { get; set; } = new GeoCoordinatesInfo();

    [JsonPropertyName("isLandlocked")]
    public bool IsLandlocked { get; set; }

    [JsonPropertyName("borderingCountries")]
    public IReadOnlyList<string> BorderingCountries { get; set; } = Array.Empty<string>();

    [JsonPropertyName("landAreaInSquareKilometers")]
    public decimal LandAreaInSquareKilometers { get; set; }

    [JsonPropertyName("emojiFlag")]
    public string EmojiFlag { get; set; } = string.Empty;

    [JsonPropertyName("demonyms")]
    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<IReadOnlyList<IDemonymn>, DemonymnInfo[]>))]
    public IReadOnlyList<IDemonymn> Demonyms { get; set; } = Array.Empty<DemonymnInfo>();

    [JsonPropertyName("flags")]
    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<IFlags, FlagsInfo>))]
    public IFlags Flags { get; set; } = new FlagsInfo();

    [JsonPropertyName("coatOfArms")]
    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<ICoatOfArms, CoatOfArmsInfo>))]
    public ICoatOfArms CoatOfArms { get; set; } = new CoatOfArmsInfo();

    [JsonPropertyName("population")]
    public decimal Population { get; set; }

    [JsonPropertyName("maps")]
    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<IMaps, MapsInfo>))]
    public IMaps Maps { get; set; } = null!;

    [JsonPropertyName("giniCoefficients")]
    public IReadOnlyDictionary<string, decimal>? GiniCoefficients { get; set; }

    [JsonPropertyName("vehicleInfo")]
    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<IVehicleInfo, VehicleInfo>))]
    public IVehicleInfo VehicleInfo { get; set; } = new VehicleInfo();

    [JsonPropertyName("postalCodeInfo")]
    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<IPostalCodeInfo, PostalCodeInfo>))]
    public IPostalCodeInfo PostalCodeInfo { get; set; } = new PostalCodeInfo();

    [JsonPropertyName("startOfWeek")]
    public string StartOfWeek { get; set; } = string.Empty;

    [JsonPropertyName("startDayOfWeek")]
    public DayOfWeek StartDayOfWeek { get; set; }

    [JsonPropertyName("timeZones")]
    public IReadOnlyList<string> TimeZones { get; set; } = Array.Empty<string>();
}