using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.Configurations;

internal class RegionHolidayConfiguration : NoxReferenceEntityConfigurationBase<RegionHoliday>
{
    protected override void DoConfigure(EntityTypeBuilder<RegionHoliday> builder)
    {
    }
}