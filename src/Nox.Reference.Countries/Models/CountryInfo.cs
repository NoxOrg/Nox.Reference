using System.Text.Json.Serialization;

namespace Nox.Reference.Countries;

internal class CountryInfo : ICountryInfo
{
    [JsonPropertyName("id")]public string Id_ { get; set; } = string.Empty;
    [JsonPropertyName("name")]public string Name_ { get; set;} = string.Empty;
    [JsonPropertyName("code")]public string Code_ { get; set; } = string.Empty;
    [JsonPropertyName("languages")] public IReadOnlyList<string> Languages_ { get; set; } = Array.Empty<string>();
    [JsonPropertyName("names")] public CountryNames Names_ { get; set; } = null!;
    [JsonPropertyName("topLevelDomains")]public IReadOnlyList<string> TopLevelDomains_ { get; set; } = Array.Empty<string>();
    [JsonPropertyName("alphaCode2")] public string AlphaCode2_ { get; set; } = string.Empty;
    [JsonPropertyName("numericCode")] public string NumericCode_ { get; set; } = string.Empty;
    [JsonPropertyName("alphaCode3")] public string AlphaCode3_ { get; set; } = string.Empty;
    [JsonPropertyName("olympicCommitteeCode")] public string OlympicCommitteeCode_ { get; set; } = string.Empty;
    [JsonPropertyName("fifaCode")] public string FifaCode_ { get; set; } = string.Empty;
    [JsonPropertyName("fipsCode")] public string FipsCode_ { get; set; } = string.Empty;
    [JsonPropertyName("isIndependent")]public bool IsIndependent_ { get; set;}
    [JsonPropertyName("codeAssignedStatus")] public string CodeAssignedStatus_ { get; set; } = string.Empty;
    [JsonPropertyName("isUnitedNationsMember")]public bool IsUnitedNationsMember_ { get; set;}
    [JsonPropertyName("currencies")] public IReadOnlyList<string> Currencies_ { get; set; } = Array.Empty<string>();
    [JsonPropertyName("dialingInfo")]public DialingInfo DialingInfo_ { get; set; } = null!;
    [JsonPropertyName("capitals")] public IReadOnlyList<string> Capitals_ { get; set; } = Array.Empty<string>();
    [JsonPropertyName("capitalInfo")] public CapitalInfo CapitalInfo_ { get; set; } = null!;
    [JsonPropertyName("alternateSpellings")] public IReadOnlyList<string> AlternateSpellings_ { get; set; } = Array.Empty<string>();
    [JsonPropertyName("region")] public string Region_ { get; set; } = string.Empty;
    [JsonPropertyName("subRegion")] public string SubRegion_ { get; set; } = string.Empty;
    [JsonPropertyName("continents")] public IReadOnlyList<string> Continents_ { get; set; } = Array.Empty<string>();
    [JsonPropertyName("nameTranslations")]public IReadOnlyList<NativeNameInfo> NameTranslations_ { get; set; } = Array.Empty<NativeNameInfo>();
    [JsonPropertyName("geoCoordinates")] public GeoCoordinates GeoCoordinates_ { get; set; } = new GeoCoordinates();
    [JsonPropertyName("isLandlocked")]public bool IsLandlocked_ { get; set;}
    [JsonPropertyName("borderingCountries")] public IReadOnlyList<string> BorderingCountries_ { get; set; } = Array.Empty<string>();
    [JsonPropertyName("landAreaInSquareKilometers")]public decimal LandAreaInSquareKilometers_ { get; set;}
    [JsonPropertyName("emojiFlag")] public string EmojiFlag_ { get; set; } = string.Empty;
    [JsonPropertyName("demonyms")] public IReadOnlyList<Demonymn> Demonyms_ { get; set; } = Array.Empty<Demonymn>();
    [JsonPropertyName("flags")] public Flags Flags_ { get; set; } = null!;
    [JsonPropertyName("coatOfArms")] public CoatOfArms CoatOfArms_ { get; set; } = null!;
    [JsonPropertyName("population")]public decimal Population_ { get; set;}
    [JsonPropertyName("maps")] public Maps Maps_ { get; set; } = null!;
    [JsonPropertyName("giniCoefficients")]public Dictionary<string, decimal>? GiniCoefficients_ { get; set;}
    [JsonPropertyName("vehicleInfo")] public VehicleInfo VehicleInfo_ { get; set; } = null!;
    [JsonPropertyName("postalCodeInfo")] public PostalCodeInfo PostalCodeInfo_ { get; set; } = null!;
    [JsonPropertyName("startOfWeek")] public string StartOfWeek_ { get; set; } = string.Empty;
    [JsonPropertyName("startDayOfWeek")]public DayOfWeek StartDayOfWeek_ { get; set;}
    [JsonPropertyName("timeZones")] public IReadOnlyList<string> TimeZones_ { get; set; } = Array.Empty<string>();

    [JsonIgnore]public string Id => Id_;
    [JsonIgnore]public string Name => Name_;
    [JsonIgnore]public string Code => Code_;
    [JsonIgnore]public IReadOnlyList<string> Languages => Languages_;
    [JsonIgnore]public ICountryNames Names => Names_;
    [JsonIgnore]public IReadOnlyList<string> TopLevelDomains => TopLevelDomains_;
    [JsonIgnore]public string AlphaCode2 => AlphaCode2_;
    [JsonIgnore]public string NumericCode => NumericCode_;
    [JsonIgnore]public string AlphaCode3 => AlphaCode3_;
    [JsonIgnore]public string OlympicCommitteeCode => OlympicCommitteeCode_;
    [JsonIgnore]public string FifaCode => FifaCode_;
    [JsonIgnore]public string FipsCode => FipsCode_;
    [JsonIgnore]public bool? IsIndependent => IsIndependent_;
    [JsonIgnore]public string CodeAssignedStatus => CodeAssignedStatus_;
    [JsonIgnore]public bool IsUnitedNationsMember => IsUnitedNationsMember_;
    [JsonIgnore]public IReadOnlyList<string>? Currencies => Currencies_;
    [JsonIgnore]public IDialingInfo? DialingInfo => DialingInfo_;
    [JsonIgnore]public IReadOnlyList<string> Capitals => Capitals_;
    [JsonIgnore]public ICapitalInfo? CapitalInfo => CapitalInfo_;
    [JsonIgnore]public IReadOnlyList<string> AlternateSpellings => AlternateSpellings_;
    [JsonIgnore]public string Region => Region_;
    [JsonIgnore]public string SubRegion => SubRegion_;
    [JsonIgnore]public IReadOnlyList<string> Continents => Continents_;
    [JsonIgnore]public IReadOnlyList<INativeNameInfo>? NameTranslations => NameTranslations_;
    [JsonIgnore]public IGeoCoordinates GeoCoordinates => GeoCoordinates_;
    [JsonIgnore]public bool IsLandlocked => IsLandlocked_;
    [JsonIgnore]public IReadOnlyList<string> BorderingCountries => BorderingCountries_;
    [JsonIgnore]public decimal LandAreaInSquareKilometers => LandAreaInSquareKilometers_;
    [JsonIgnore]public string EmojiFlag => EmojiFlag_;
    [JsonIgnore]public IReadOnlyList<IDemonymn>? Demonyms => Demonyms_;
    [JsonIgnore]public IFlags Flags => Flags_;
    [JsonIgnore]public ICoatOfArms CoatOfArms => CoatOfArms_;
    [JsonIgnore]public decimal Population => Population_;
    [JsonIgnore]public IMaps Maps => Maps_;
    [JsonIgnore]public IReadOnlyDictionary<string, decimal>? GiniCoefficients => GiniCoefficients_;
    [JsonIgnore]public IVehicleInfo? VehicleInfo => VehicleInfo_;
    [JsonIgnore]public IPostalCodeInfo? PostalCodeInfo => PostalCodeInfo_;
    [JsonIgnore]public string StartOfWeek => StartOfWeek_;
    [JsonIgnore]public DayOfWeek StartDayOfWeek => StartDayOfWeek_;
    [JsonIgnore]public IReadOnlyList<string> TimeZones => TimeZones_;

}
