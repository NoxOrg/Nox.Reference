using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

public class RestcountryCountryInfo : ICountryInfo
{
    [JsonIgnore]
    public string Id => AlphaCode2;

    [JsonIgnore]
    public string Name => Names.CommonName;

    [JsonPropertyName("name")]
    public RestcountryCountryNames Names_ { get; set; } = null!;

    [JsonIgnore]
    public ICountryNames Names => Names_;

    [JsonPropertyName("tld")]
    public IReadOnlyList<string> TopLevelDomains { get; set; } = new List<string>();

    [JsonPropertyName("cca2")]
    public string AlphaCode2 { get; set; } = string.Empty;

    [JsonPropertyName("ccn3")]
    public string NumericCode { get; set; } = string.Empty;

    [JsonPropertyName("cca3")]
    public string AlphaCode3 { get; set; } = string.Empty;

    [JsonIgnore]
    public string Code => AlphaCode3;

    [JsonPropertyName("cioc")]
    public string OlympicCommitteeCode { get; set; } = string.Empty;

    [JsonPropertyName("fifa")]
    public string FifaCode { get; set; } = string.Empty;

    public string FipsCode { get; set; } = string.Empty;

    [JsonPropertyName("independent")]
    public bool? IsIndependent { get; set; } = true;

    [JsonPropertyName("status")]
    public string CodeAssignedStatus { get; set; } = string.Empty;

    [JsonPropertyName("unMember")]
    public bool IsUnitedNationsMember { get; set; } = true;

    [JsonPropertyName("currencies")]
    public Dictionary<string, RestcountryCurrencyInfo>? Currencies1 { get; set; }

    [JsonIgnore]
    public IReadOnlyList<string>? Currencies => Currencies1?.Select(kv => kv.Key).ToList();

    [JsonPropertyName("idd")]
    public RestcountryDialingInfo? DialingInfo_ { get; set; }

    [JsonIgnore]
    public IDialingInfo? DialingInfo => DialingInfo_;

    [JsonPropertyName("capital")]
    public IReadOnlyList<string> Capitals { get; set; } = new List<string>();

    [JsonPropertyName("capitalInfo")]
    public RestcountryCapitalInfo? CapitalInfo_ { get; set; }

    [JsonIgnore]
    public ICapitalInfo? CapitalInfo => CapitalInfo_;

    [JsonPropertyName("altSpellings")]
    public IReadOnlyList<string> AlternateSpellings { get; set; } = new List<string>();

    [JsonPropertyName("region")]
    public string Region { get; set; } = string.Empty;

    [JsonPropertyName("subregion")]
    public string SubRegion { get; set; } = string.Empty;

    [JsonPropertyName("continents")]
    public IReadOnlyList<string> Continents { get; set; } = new List<string>();

    [JsonPropertyName("languages")]
    public Dictionary<string, string> Languages_ { get; set; } = new Dictionary<string, string>();

    public IReadOnlyList<string> Languages => Languages_.Select(kv => kv.Key).ToList();

    [JsonPropertyName("translations")]
    public Dictionary<string, RestcountryNativeNameInfo> NameTranslations_ { get; set; } = new Dictionary<string, RestcountryNativeNameInfo>();

    [JsonIgnore]
    public IReadOnlyList<INativeNameInfo>? NameTranslations => NameTranslations_?
        .Select(kv => new RestcountryNativeNameInfo
        {
            Language = kv.Key,
            CommonName = kv.Value.CommonName,
            OfficialName = kv.Value.OfficialName,
        }).ToList();

    [JsonPropertyName("latlng")]
    public IReadOnlyList<decimal> LatLong { get; set; } = null!;

    [JsonPropertyName("geoCoordinates")]
    public IGeoCoordinates GeoCoordinates { get; set; } = new GeoCoordinatesInfo();

    [JsonPropertyName("landlocked")]
    public bool IsLandlocked { get; set; }

    [JsonPropertyName("borders")]
    public IReadOnlyList<string> BorderingCountries { get; set; } = new List<string>();

    [JsonPropertyName("area")]
    public decimal LandAreaInSquareKilometers { get; set; }

    [JsonPropertyName("flag")]
    public string EmojiFlag { get; set; } = string.Empty;

    [JsonPropertyName("demonyms")]
    public Dictionary<string, RestcountryDemonymn>? Demonyms_ { get; set; } = new Dictionary<string, RestcountryDemonymn>();

    [JsonIgnore]
    public IReadOnlyList<IDemonymn>? Demonyms => Demonyms_?
        .Select(kv => new RestcountryDemonymn
        {
            Language = kv.Key,
            Masculine = kv.Value.Masculine,
            Feminine = kv.Value.Feminine,
        }).ToList();

    [JsonPropertyName("flags")]
    public RestcountryFlags Flags_ { get; set; } = null!;

    [JsonIgnore]
    public IFlags Flags => Flags_;

    [JsonPropertyName("coatOfArms")]
    public RestcountryCoatOfArms CoatOfArms_ { get; set; } = null!;

    [JsonIgnore]
    public ICoatOfArms CoatOfArms => CoatOfArms_;

    [JsonPropertyName("population")]
    public decimal Population { get; set; }

    [JsonPropertyName("maps")]
    public RestcountryMaps Maps1 { get; set; } = null!;

    [JsonIgnore]
    public IMaps Maps => Maps1;

    [JsonPropertyName("gini")]
    public Dictionary<string, decimal>? GiniCoefficients_ { get; set; }

    [JsonIgnore]
    public IReadOnlyDictionary<string, decimal>? GiniCoefficients => GiniCoefficients_;

    [JsonPropertyName("car")]
    public RestcountryVehicleInfo? VehicleInfo_ { get; set; }

    [JsonIgnore]
    public IVehicleInfo? VehicleInfo => VehicleInfo_;

    [JsonPropertyName("postalCode")]
    public RestcountryPostalCodeInfo? PostalCodeInfo1 { get; set; }

    [JsonIgnore]
    public IPostalCodeInfo? PostalCodeInfo => PostalCodeInfo1;

    [JsonPropertyName("startOfWeek")]
    public string StartOfWeek { get; set; } = "monday";

    [JsonIgnore]
    public DayOfWeek StartDayOfWeek => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), StartOfWeek, true);

    [JsonPropertyName("timezones")]
    public IReadOnlyList<string> TimeZones { get; set; } = new List<string>();
}