using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations;

internal class TimeZoneConfiguration : NoxReferenceEntityConfigurationBase<TimeZone>
{
    protected override void DoConfigure(EntityTypeBuilder<TimeZone> builder)
    {
        builder
            .HasMany(x => x.Countries)
            .WithMany(x => x.TimeZones);
    }
}