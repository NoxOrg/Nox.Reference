using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

internal class CountryInfo
{
    [JsonIgnore]
    public string Id => AlphaCode2;

    [JsonIgnore]
    public string Name => Names.CommonName;

    [JsonPropertyName("name")]
    public CountryNamesInfo Names { get; set; } = new CountryNamesInfo();

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
    public Dictionary<string, CountryCurrencyInfo> Currencies1 { get; set; } = new Dictionary<string, CountryCurrencyInfo>();

    [JsonIgnore]
    public IReadOnlyList<string> Currencies => Currencies1.Select(kv => kv.Key).ToList();

    [JsonPropertyName("idd")]
    public DialingInfo? DialingInfo { get; set; }

    [JsonPropertyName("capital")]
    public IReadOnlyList<string> Capitals { get; set; } = new List<string>();

    [JsonPropertyName("capitalInfo")]
    public CapitalInfo? CapitalInfo { get; set; }

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

    [JsonIgnore]
    public IReadOnlyList<string> Languages => Languages_.Select(kv => kv.Key).ToList();

    [JsonPropertyName("translations")]
    public Dictionary<string, NativeNameInfo> NameTranslations_ { get; set; } = new Dictionary<string, NativeNameInfo>();

    [JsonIgnore]
    public IReadOnlyList<NativeNameInfo>? NameTranslations => NameTranslations_?
        .Select(kv => new NativeNameInfo
        {
            Language = kv.Key,
            CommonName = kv.Value.CommonName,
            OfficialName = kv.Value.OfficialName,
        }).ToList();

    [JsonPropertyName("latlng")]
    public IReadOnlyList<decimal> LatLong { get; set; } = null!;

    [JsonPropertyName("geoCoordinates")]
    public GeoCoordinatesInfo GeoCoordinates { get; set; } = new GeoCoordinatesInfo();

    [JsonPropertyName("landlocked")]
    public bool IsLandlocked { get; set; }

    [JsonPropertyName("borders")]
    public IReadOnlyList<string> BorderingCountries { get; set; } = new List<string>();

    [JsonPropertyName("area")]
    public decimal LandAreaInSquareKilometers { get; set; }

    [JsonPropertyName("flag")]
    public string EmojiFlag { get; set; } = string.Empty;

    [JsonPropertyName("demonyms")]
    public Dictionary<string, DemonymnInfo>? Demonyms_ { get; set; } = new Dictionary<string, DemonymnInfo>();

    [JsonIgnore]
    public IReadOnlyList<DemonymnInfo>? Demonyms => Demonyms_?
        .Select(kv => new DemonymnInfo
        {
            Language = kv.Key,
            Masculine = kv.Value.Masculine,
            Feminine = kv.Value.Feminine,
        }).ToList();

    [JsonPropertyName("flags")]
    public FlagsInfo Flags { get; set; } = new FlagsInfo();

    [JsonPropertyName("coatOfArms")]
    public CoatOfArmsInfo CoatOfArms { get; set; } = new CoatOfArmsInfo();

    [JsonPropertyName("population")]
    public decimal Population { get; set; }

    [JsonPropertyName("maps")]
    public MapsInfo Maps { get; set; } = new MapsInfo();

    [JsonPropertyName("gini")]
    public Dictionary<int, decimal>? GiniCoefficients_ { get; set; }

    [JsonIgnore]
    public IReadOnlyDictionary<int, decimal>? GiniCoefficients => GiniCoefficients_;

    [JsonPropertyName("car")]
    public VehicleInfo? VehicleInfo { get; set; }

    [JsonPropertyName("postalCode")]
    public PostalCodeInfo? PostalCodeInfo { get; set; }

    [JsonPropertyName("startOfWeek")]
    public string StartOfWeek { get; set; } = "monday";

    [JsonIgnore]
    public DayOfWeek StartDayOfWeek => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), StartOfWeek, true);

    [JsonPropertyName("timezones")]
    public IReadOnlyList<string> TimeZones { get; set; } = new List<string>();
}