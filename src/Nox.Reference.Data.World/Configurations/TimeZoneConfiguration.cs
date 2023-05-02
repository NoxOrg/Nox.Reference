using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;
using Nox.Reference.Data.World.Entities.Cultures;

namespace Nox.Reference.Data.World.Configurations;

internal class TimeZoneConfiguration : NoxReferenceEntityConfigurationBase<Entities.TimeZones.TimeZone>
{
    protected override void DoConfigure(EntityTypeBuilder<Entities.TimeZones.TimeZone> builder)
    {
        // TODO: add when currency entity is added
        //builder
        //    .HasOne(x => x.DateFormat)
        //    .WithOne(x => x.Culture)
        //    .HasForeignKey<DateFormat>("CultureId")
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}