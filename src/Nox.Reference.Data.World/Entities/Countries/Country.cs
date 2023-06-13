using Nox.Reference.Data.Common;
using Nox.Reference.Data.World.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nox.Reference.Data.World;

public class Country : NoxReferenceEntityBase,
    IKeyedNoxReferenceEntity<string>,
    IDtoConvertibleEntity<CountryInfo>
{
    public string Id => Code;
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;

    public virtual CountryNames Names { get; internal set; } = new CountryNames();
    public virtual IReadOnlyList<TopLevelDomain> TopLevelDomains { get; internal set; } = new List<TopLevelDomain>();
    public virtual IReadOnlyList<Language> Languages { get; internal set; } = new List<Language>();
    public virtual IReadOnlyList<Currency> Currencies { get; internal set; } = new List<Currency>();
    public virtual IReadOnlyList<AlternateSpelling> AlternateSpellings { get; internal set; } = new List<AlternateSpelling>();
    public virtual IReadOnlyList<Continent> Continents { get; internal set; } = new List<Continent>();
    public virtual IReadOnlyList<CountryNameTranslation> NameTranslations { get; internal set; } = new List<CountryNameTranslation>();
    public virtual IReadOnlyList<GiniCoefficient> GiniCoefficients { get; internal set; } = new List<GiniCoefficient>();
    public virtual IReadOnlyList<Demonymn> Demonyms { get; internal set; } = new List<Demonymn>();
    public virtual List<Country> BorderingCountries { get; internal set; } = new List<Country>();
    public virtual IReadOnlyList<CountryCapital> Capitals { get; internal set; } = new List<CountryCapital>();
    public virtual IReadOnlyList<TimeZone> TimeZones { get; private set; } = new List<TimeZone>();
    public virtual List<Culture> Cultures { get; private set; } = new List<Culture>();

    [NotMapped]
    public CountryCapital Capital => Capitals.FirstOrDefault() ?? new CountryCapital();

    internal int? VatNumberDefinitionId { get; private set; }

    [ForeignKey("VatNumberDefinitionId")]
    public virtual VatNumberDefinition? VatNumberDefinition { get; internal set; }

    public virtual CountryDialing? Dialing { get; private set; }
    public virtual CoatOfArms? CoatOfArms { get; private set; }
    public virtual GeoCoordinates? GeoCoordinates { get; private set; }
    public virtual CountryFlag? Flag { get; private set; }
    public virtual CountryMaps? Maps { get; private set; }
    public virtual CountryVehicle? Vehicle { get; private set; }
    public virtual PostalCode? PostalCode { get; private set; }
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

    public CountryInfo ToDto()
    {
        return World.Mapper.Map<CountryInfo>(this);
    }
}