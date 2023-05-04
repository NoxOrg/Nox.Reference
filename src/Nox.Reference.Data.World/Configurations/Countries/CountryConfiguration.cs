﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.Countries;

internal class CountryConfiguration : NoxReferenceEntityConfigurationBase<Country>
{
    protected override void DoConfigure(EntityTypeBuilder<Country> builder)
    {
        builder
            .HasOne(x => x.Dialing)
            .WithMany();

        builder
            .HasOne(x => x.CoatOfArms)
            .WithMany();

        builder
            .HasOne(x => x.GeoCoordinates)
            .WithMany();

        builder
            .HasOne(x => x.Flag)
            .WithMany();

        builder
            .HasOne(x => x.Maps)
            .WithMany();

        builder
            .HasOne(x => x.Vehicle)
            .WithMany();

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
            .HasOne(x => x.Names)
            .WithMany();

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