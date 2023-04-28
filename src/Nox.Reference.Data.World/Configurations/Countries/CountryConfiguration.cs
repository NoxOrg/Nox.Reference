using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.Countries;

internal class CountryConfiguration : NoxReferenceEntityConfigurationBase<Country>
{
    protected override void DoConfigure(EntityTypeBuilder<Country> builder)
    {
        builder
            .HasMany(x => x.Languages)
            .WithOne();

        builder
            .HasMany(x => x.TopLevelDomains)
            .WithOne();

        builder
            .Ows(x => x.Capital)
            .WithOne();

        builder
            .HasMany(x => x.Capitals)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Flags)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.TimeZones)
            .WithMany()
            .UsingEntity(j => j.ToTable("CountryTimeZoneInfos"));

        builder.HasMany(x => x.Currencies)
            .WithMany()
            .UsingEntity(j => j.ToTable("CountryCurrencies"));

        /*
             public IReadOnlyList<CountryNativeName> NativeNames { get; set; } = Array.Empty<CountryNativeName>();
    public IReadOnlyList<string> TopLevelDomains { get; set; } = Array.Empty<string>();
    public IReadOnlyList<string> Languages { get; set; } = Array.Empty<string>();
    public IReadOnlyList<string> Currencies { get; set; } = Array.Empty<string>();
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
         */
    }
}