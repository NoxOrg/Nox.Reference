using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations;

internal class RegionHolidayConfiguration : NoxReferenceEntityConfigurationBase<RegionHoliday>
{
    protected override void DoConfigure(EntityTypeBuilder<RegionHoliday> builder)
    {
        builder.HasMany(x => x.Holidays)
            .WithOne();
    }
}