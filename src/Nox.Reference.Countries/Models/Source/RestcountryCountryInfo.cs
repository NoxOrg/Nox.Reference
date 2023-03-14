
using System.Text.Json.Serialization;

namespace Nox.Reference.Countries;

public class RestcountryCountryInfo : ICountryInfo
{
    [JsonIgnore]
    public int Id => Convert.ToInt32(NumericCode);

    [JsonIgnore]
    public string Name => Names.CommonName;

    [JsonPropertyName("name")]
    public RestcountryCountryNames Names1 { get; set; } = null!;

    [JsonIgnore]
    public ICountryNames Names => Names1; 

    [JsonPropertyName("tld")]
    public string[] TopLevelDomains { get; set; } = Array.Empty<string>();

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
    public RestcountryDialingInfo? DialingInfo1 { get; set; }

    [JsonIgnore]
    public IDialingInfo? DialingInfo => DialingInfo1;

    [JsonPropertyName("capital")]
    public string[] Capitals { get; set; } = Array.Empty<string>();

    [JsonPropertyName("capitalInfo")]
    public RestcountryCapitalInfo? CapitalInfo1 { get; set; }

    [JsonIgnore]
    public ICapitalInfo? CapitalInfo => CapitalInfo1;

    [JsonPropertyName("altSpellings")]
    public string[] AlternateSpellings { get; set; } = Array.Empty<string>();

    [JsonPropertyName("region")]
    public string Region { get; set; } = string.Empty;

    [JsonPropertyName("subregion")]
    public string SubRegion { get; set; } = string.Empty;

    [JsonPropertyName("continents")]
    public string[] Continents { get; set; } = Array.Empty<string>();

    [JsonPropertyName("languages")]
    public Dictionary<string, string> Languages1 { get; set; } = null!;
    public IReadOnlyList<string> Languages => Languages1.Select(kv => kv.Key).ToList();

    [JsonPropertyName("translations")]
    public Dictionary<string, RestcountryNativeNameInfo>? NameTranslations1 { get; set; } = null!;

    [JsonIgnore]
    public IReadOnlyList<INativeNameInfo>? NameTranslations => NameTranslations1?
        .Select(kv => new RestcountryNativeNameInfo
        {
            Language = kv.Key,
            CommonName = kv.Value.CommonName,
            OfficialName = kv.Value.OfficialName,
        }).ToList();

    [JsonPropertyName("latlng")]
    public decimal[] LatLong { get; set; } = null!;

    [JsonPropertyName("landlocked")]
    public bool IsLandlocked { get; set; }

    [JsonPropertyName("borders")]
    public string[] BorderingCountries { get; set; } = Array.Empty<string>();

    [JsonPropertyName("area")]
    public decimal LandAreaInSquareKilometers { get; set; }

    [JsonPropertyName("flag")]
    public string EmojiFlag { get; set; } = string.Empty;

    [JsonPropertyName("demonyms")]
    public Dictionary<string, RestcountryDemonymn>? Demonyms1 { get; set; } = null!;

    [JsonIgnore]
    public IReadOnlyList<IDemonymn>? Demonyms => Demonyms1?
        .Select(kv => new RestcountryDemonymn
        {
            Language = kv.Key,
            Masculine = kv.Value.Masculine,
            Feminine = kv.Value.Feminine,
        }).ToList();

    [JsonPropertyName("flags")]
    public RestcountryFlags Flags1 { get; set; } = null!;

    [JsonIgnore]
    public IFlags Flags  => Flags1;

    [JsonPropertyName("coatOfArms")]
    public RestcountryCoatOfArms CoatOfArms1 { get; set; } = null!;

    [JsonIgnore]
    public ICoatOfArms CoatOfArms => CoatOfArms1;


    [JsonPropertyName("population")]
    public decimal Population { get; set; }

    [JsonPropertyName("maps")]
    public RestcountryMaps Maps1 { get; set; } = null!;

    [JsonIgnore]
    public IMaps Maps => Maps1;

    [JsonPropertyName("gini")]
    public Dictionary<string, decimal>? GiniCoefficients1 { get; set; }

    [JsonIgnore]
    public IReadOnlyDictionary<string, decimal>? GiniCoefficients => GiniCoefficients1;

    [JsonPropertyName("car")]
    public RestcountryVehicleInfo? VehicleInfo1 { get; set; }

    [JsonIgnore]
    public IVehicleInfo? VehicleInfo => VehicleInfo1;

    [JsonPropertyName("postalCode")]
    public RestcountryPostalCodeInfo? PostalCodeInfo1 { get; set; }

    [JsonIgnore]
    public IPostalCodeInfo? PostalCodeInfo => PostalCodeInfo1;

    [JsonPropertyName("startOfWeek")]
    public string StartOfWeek { get; set; } = "monday";

    [JsonIgnore]
    public DayOfWeek StartDayOfWeek => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), StartOfWeek, true);

    [JsonPropertyName("timezones")]
    public string[] TimeZones { get; set; } = Array.Empty<string>();
}
