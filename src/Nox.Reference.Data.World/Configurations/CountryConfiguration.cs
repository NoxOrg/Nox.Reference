using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations;

internal class CountryConfiguration : NoxReferenceEntityConfigurationBase<Country>
{
    protected override void DoConfigure(EntityTypeBuilder<Country> builder)
    {
        builder
            .HasMany(x => x.Languages)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.TopLevelDomains)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Capital)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

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
    }
}