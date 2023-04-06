using System;
using System.Collections.Generic;

namespace Nox.Reference.Repository;

public partial class Country
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string CommonName { get; set; } = null!;

    public string OfficialName { get; set; } = null!;

    public string AlphaCode2 { get; set; } = null!;

    public short NumericCode { get; set; }

    public string AlphaCode3 { get; set; } = null!;

    public string OlympicCommitteeCode { get; set; } = null!;

    public string FifaCode { get; set; } = null!;

    public string FipsCode { get; set; } = null!;

    public bool IsIndependent { get; set; }

    public string CodeAssignedStatus { get; set; } = null!;

    public bool IsUnitedNationsMember { get; set; }

    public string Region { get; set; } = null!;

    public string? SubRegion { get; set; }

    public bool IsLandLockec { get; set; }

    public int? LandAreaInSquareKilometers { get; set; }

    public string? EmojiFlag { get; set; }

    public virtual ICollection<CountryAlternateSpelling> CountryAlternateSpellings { get; } = new List<CountryAlternateSpelling>();

    public virtual ICollection<CountryNameTranslation> CountryNameTranslations { get; } = new List<CountryNameTranslation>();

    public virtual ICollection<CountryNativeName> CountryNativeNames { get; } = new List<CountryNativeName>();

    public virtual ICollection<DialingInfo> DialingInfos { get; } = new List<DialingInfo>();

    public virtual ICollection<Country> BorderingCountries { get; } = new List<Country>();

    public virtual ICollection<Continent> Continents { get; } = new List<Continent>();

    public virtual ICollection<Country> Countries { get; } = new List<Country>();

    public virtual ICollection<Currency> Currencies { get; } = new List<Currency>();

    public virtual ICollection<Demonym> Demonyms { get; } = new List<Demonym>();

    public virtual ICollection<DomainNameExtension> DomainNameExtensions { get; } = new List<DomainNameExtension>();

    public virtual ICollection<GeoPlace> GeoPlaces { get; } = new List<GeoPlace>();

    public virtual ICollection<Language> Languages { get; } = new List<Language>();
}
