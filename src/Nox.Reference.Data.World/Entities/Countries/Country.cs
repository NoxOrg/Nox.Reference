using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class Country : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;

    public CountryNames Names { get; private set; } = new CountryNames();
    public IReadOnlyList<TopLevelDomain> TopLevelDomains { get; internal set; } = new List<TopLevelDomain>();
    public IReadOnlyList<Language> Languages { get; internal set; } = new List<Language>();
    public IReadOnlyList<Currency> Currencies { get; internal set; } = new List<Currency>();
    public IReadOnlyList<AlternateSpelling> AlternateSpellings { get; internal set; } = new List<AlternateSpelling>();
    public IReadOnlyList<Continent> Continents { get; internal set; } = new List<Continent>();
    public IReadOnlyList<CountryNameTranslation> NameTranslations { get; internal set; } = new List<CountryNameTranslation>();
    public IReadOnlyList<GiniCoefficient> GiniCoefficients { get; internal set; } = new List<GiniCoefficient>();
    public IReadOnlyList<Demonymn> Demonyms { get; internal set; } = new List<Demonymn>();
    public IReadOnlyList<Country> BorderingCountries { get; internal set; } = new List<Country>();
    public IReadOnlyList<CountryCapital> Capitals { get; internal set; } = new List<CountryCapital>();

    public CountryDialing? Dialing { get; private set; }
    public CountryCapital Capital => Capitals.FirstOrDefault() ?? new CountryCapital();
    public CoatOfArms? CoatOfArms { get; private set; }
    public GeoCoordinates? GeoCoordinates { get; private set; }
    public CountryFlag? Flag { get; private set; }
    public CountryMaps? Maps { get; private set; }
    public CountryVehicle? Vehicle { get; private set; }
    public PostalCode? PostalCode { get; private set; }
    public string EmojiFlag { get; private set; } = string.Empty;
    public decimal LandAreaInSquareKilometers { get; private set; }
    public bool? IsIndependent { get; private set; }
    public bool IsUnitedNationsMember { get; private set; }
    public string Region { get; private set; } = string.Empty;
    public string SubRegion { get; private set; } = string.Empty;
    public bool IsLandlocked { get; private set; }
    public decimal Population { get; private set; }
    public string StartOfWeek { get; private set; } = string.Empty;
    public string AlphaCode2 { get; private set; } = string.Empty;
    public string NumericCode { get; private set; } = string.Empty;
    public string AlphaCode3 { get; private set; } = string.Empty;
    public string OlympicCommitteeCode { get; private set; } = string.Empty;
    public string FifaCode { get; private set; } = string.Empty;
    public string FipsCode { get; private set; } = string.Empty;
    public string CodeAssignedStatus { get; private set; } = string.Empty;
    public DayOfWeek StartDayOfWeek { get; private set; }
}