using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.Countries;

internal class CountryConfiguration : NoxReferenceEntityConfigurationBase<Country>
{
    protected override void DoConfigure(EntityTypeBuilder<Country> builder)
    {
        builder
            .OwnsOne(x => x.Dialing)
            .WithOwner();

        builder
            .OwnsOne(x => x.CoatOfArms)
            .WithOwner();

        builder
            .HasOne(x => x.GeoCoordinates)
            .WithMany();

        builder
            .OwnsOne(x => x.Flag)
            .WithOwner();

        builder
            .OwnsOne(x => x.Maps)
            .WithOwner();

        builder
            .OwnsOne(x => x.Vehicle)
            .WithOwner();

        builder
            .HasOne(x => x.PostalCode)
            .WithMany();

        builder
            .HasMany(x => x.TopLevelDomains)
            .WithMany();

        builder
            .Ignore(x => x.Capital);

        builder
            .HasMany(x => x.Capitals)
            .WithOne();

        builder
            .HasMany(x => x.NativeNames)
            .WithOne();

        builder
            .HasMany(x => x.NameTranslations)
            .WithOne();

        builder
            .HasMany(x => x.Languages)
            .WithMany();

        builder
            .HasMany(x => x.Currencies)
            .WithMany();

        builder
            .HasMany(x => x.BorderingCountries)
            .WithMany();

        builder
            .HasMany(x => x.Demonyms)
            .WithMany();

        builder
            .HasMany(x => x.Continents)
            .WithMany();

        builder
            .HasMany(x => x.AlternateSpellings)
            .WithOne();

        builder
            .HasMany(x => x.NameTranslations)
            .WithOne();

        builder
            .HasMany(x => x.GiniCoefficients)
            .WithOne();
    }
}