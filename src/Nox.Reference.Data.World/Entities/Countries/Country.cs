﻿using Nox.Reference.Data.Common;

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
    public IReadOnlyList<Currency> Currencies { get; set; } = Array.Empty<Currency>();
    public IReadOnlyList<AlternateSpelling> AlternateSpellings { get; set; } = Array.Empty<AlternateSpelling>();
    public IReadOnlyList<Continent> Continents { get; set; } = Array.Empty<Continent>();
    public IReadOnlyList<CountryNameTranslation> NameTranslations { get; set; } = Array.Empty<CountryNameTranslation>();
    public IReadOnlyList<GiniCoefficient> GiniCoefficients { get; set; } = Array.Empty<GiniCoefficient>();
    public IReadOnlyList<Demonymn> Demonyms { get; set; } = Array.Empty<Demonymn>();
    public IReadOnlyList<Country> BorderingCountries { get; set; } = Array.Empty<Country>();
    public IReadOnlyList<CountryCapital> Capitals { get; set; } = Array.Empty<CountryCapital>();

    public CountryDialing Dialing { get; set; } = new CountryDialing();
    public CountryCapital Capital => Capitals[0];
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