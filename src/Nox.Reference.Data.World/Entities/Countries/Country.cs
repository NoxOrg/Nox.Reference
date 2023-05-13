using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class Country : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

    public CountryNames Names { get; set; } = new CountryNames();
    public IReadOnlyList<TopLevelDomain> TopLevelDomains { get; set; } = Array.Empty<TopLevelDomain>();
    public IReadOnlyList<Language> Languages { get; set; } = Array.Empty<Language>();
    public IReadOnlyList<Currency> Currencies { get; set; } = Array.Empty<Currency>();
    public IReadOnlyList<AlternateSpelling> AlternateSpellings { get; set; } = Array.Empty<AlternateSpelling>();
    public IReadOnlyList<Continent> Continents { get; set; } = Array.Empty<Continent>();
    public IReadOnlyList<CountryNameTranslation> NameTranslations { get; set; } = Array.Empty<CountryNameTranslation>();
    public IReadOnlyList<GiniCoefficient> GiniCoefficients { get; set; } = Array.Empty<GiniCoefficient>();
    public IReadOnlyList<Demonymn> Demonyms { get; set; } = Array.Empty<Demonymn>();
    public IReadOnlyList<Country> BorderingCountries { get; set; } = Array.Empty<Country>();
    public IReadOnlyList<CountryCapital> Capitals { get; set; } = Array.Empty<CountryCapital>();
    public IReadOnlyList<TimeZone> TimeZones { get; set; } = Array.Empty<TimeZone>();

    public CountryDialing? Dialing { get; set; }
    public CountryCapital Capital => Capitals.FirstOrDefault() ?? new CountryCapital();
    public CoatOfArms? CoatOfArms { get; set; }
    public GeoCoordinates? GeoCoordinates { get; set; }
    public CountryFlag? Flag { get; set; }
    public CountryMaps? Maps { get; set; }
    public CountryVehicle? Vehicle { get; set; }
    public PostalCode? PostalCode { get; set; }

    public string EmojiFlag { get; set; } = string.Empty;
    public decimal LandAreaInSquareKilometers { get; set; }
    public bool? IsIndependent { get; set; }
    public bool IsUnitedNationsMember { get; set; }

    public string Region { get; set; } = string.Empty;
    public string SubRegion { get; set; } = string.Empty;
    public bool IsLandlocked { get; set; }
    public decimal Population { get; set; }
    public string StartOfWeek { get; set; } = string.Empty;
    public string AlphaCode2 { get; set; } = string.Empty;
    public string NumericCode { get; set; } = string.Empty;
    public string AlphaCode3 { get; set; } = string.Empty;
    public string OlympicCommitteeCode { get; set; } = string.Empty;
    public string FifaCode { get; set; } = string.Empty;
    public string FipsCode { get; set; } = string.Empty;
    public string CodeAssignedStatus { get; set; } = string.Empty;
    public DayOfWeek StartDayOfWeek { get; set; }
}