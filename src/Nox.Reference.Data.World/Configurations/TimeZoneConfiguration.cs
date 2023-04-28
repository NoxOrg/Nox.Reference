using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations;

internal class TimeZoneConfiguration : NoxReferenceEntityConfigurationBase<TimeZone>
{
    protected override void DoConfigure(EntityTypeBuilder<TimeZone> builder)
    {
        // TODO: add when currency entity is added
        //builder
        //    .HasOne(x => x.DateFormat)
        //    .WithOne(x => x.Culture)
        //    .HasForeignKey<DateFormat>("CultureId")
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}