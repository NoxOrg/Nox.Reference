using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class Country : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string CommonName { get; set; } = string.Empty;
    public string OfficialName { get; set; } = string.Empty;

    public IReadOnlyList<CountryNativeName> NativeNames { get; set; } = Array.Empty<CountryNativeName>();
    public IReadOnlyList<TopLevelDomain> TopLevelDomains { get; set; } = Array.Empty<TopLevelDomain>();
    public IReadOnlyList<Language> Languages { get; set; } = Array.Empty<Language>();
    public IReadOnlyList<Currency> Currencies { get; set; } = Array.Empty<string>();
    public IReadOnlyList<string> AlternateSpellings { get; set; } = Array.Empty<string>();
    public IReadOnlyList<string> Continents { get; set; } = Array.Empty<string>();
    public IReadOnlyList<CountryNativeName> NameTranslations { get; set; } = Array.Empty<CountryNativeName>();
    public IReadOnlyList<GiniCoefficient> GiniCoefficients { get; set; } = Array.Empty<GiniCoefficient>();
    public IReadOnlyList<Demonymn> Demonyms { get; set; } = Array.Empty<Demonymn>();
    public IReadOnlyList<string> BorderingCountries { get; set; } = Array.Empty<string>();
    public IReadOnlyList<string> Capitals { get; set; } = Array.Empty<string>();

    public CountryDialing Dialing { get; set; } = new CountryDialing();
    public CountryCapital Capital { get; set; } = new CountryCapital();
    public CoatOfArms CoatOfArms { get; set; } = new CoatOfArms();
    public GeoCoordinates GeoCoordinates { get; set; } = new GeoCoordinates();
    public CountryFlag Flag { get; set; } = new CountryFlag();
    public CountryMaps Maps { get; set; } = new CountryMaps();
    public Vehicle Vehicle { get; set; } = new Vehicle();
    public PostalCode PostalCode { get; set; } = new PostalCode();

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

internal class TopLevelDomain : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Name { get; set; }
}

internal class CountryNativeName : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Language { get; set; } = string.Empty;
    public string OfficialName { get; set; } = string.Empty;
    public string CommonName { get; set; } = string.Empty;
}

internal class CountryDialing
{
    public string Prefix { get; set; } = string.Empty;

    public IReadOnlyList<string> Suffixes { get; set; } = new List<string>();
}

internal class CountryCapital
{
    public string Name { get; set; } = string.Empty;
    public GeoCoordinates GeoCoordinates { get; set; } = new GeoCoordinates();
}

internal class CoatOfArms
{
    public string Svg { get; set; } = string.Empty;

    public string Png { get; set; } = string.Empty;
}

internal class CountryMaps
{
    public string GoogleMaps { get; set; } = string.Empty;

    public string OpenStreetMaps { get; set; } = string.Empty;
}