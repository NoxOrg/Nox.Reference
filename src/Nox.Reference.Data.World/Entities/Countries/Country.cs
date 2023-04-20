using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class Country : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public IReadOnlyList<Language> Languages { get; set; } = new List<Language>();
    public IReadOnlyList<TopLevelDomain> TopLevelDomains { get; set; } = new List<TopLevelDomain>();
    public string AlphaCode2 { get; set; } = string.Empty;
    public string NumericCode { get; set; } = string.Empty;
    public string AlphaCode3 { get; set; } = string.Empty;
    public string OlympicCommitteeCode { get; set; } = string.Empty;
    public string FifaCode { get; set; } = string.Empty;
    public string FipsCode { get; set; } = string.Empty;
    public bool? IsIndependent { get; set; }
    public string CodeAssignedStatus { get; set; } = string.Empty;
    public bool IsUnitedNationsMember { get; set; }
    public IReadOnlyList<Currency> Currencies { get; set; } = new List<Currency>();
    public Dialing Dialing { get; set; } = new Dialing();
    public IReadOnlyList<City> Capitals { get; set; } = new List<City>();
    public City Capital { get; set; } = new City();
    public string Region { get; set; } = string.Empty;
    public string SubRegion { get; set; } = string.Empty;
    public IReadOnlyList<string> Continents { get; set; } = new List<string>();
    public GeoCoordinates GeoCoordinates { get; set; } = new GeoCoordinates();
    public bool IsLandlocked { get; private set; }
    public IReadOnlyList<Country> BorderingCountries { get; set; } = new List<Country>();
    public decimal LandAreaInSquareKilometers { get; set; }
    public string EmojiFlag { get; set; } = string.Empty;
    public IReadOnlyList<Demonymn>? Demonyms { get; set; }
    public Flags Flags { get; private set; } = new Flags();
    public CoatOfArms CoatOfArms { get; set; } = new CoatOfArms();
    public decimal Population { get; set; }
    public Maps Maps { get; private set; } = new Maps();
    public IReadOnlyList<GiniCoefficient> GiniCoefficients { get; set; } = new List<GiniCoefficient>();
    public Vehicle? Vehicle { get; set; }
    public string StartOfWeek { get; set; } = string.Empty;
    public DayOfWeek StartDayOfWeek { get; private set; }
    public IReadOnlyList<TimeZoneInfo> TimeZones { get; set; } = new List<TimeZoneInfo>();
}