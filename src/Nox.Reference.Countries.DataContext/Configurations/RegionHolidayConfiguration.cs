using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext.Configurations;

internal class RegionHolidayConfiguration : NoxReferenceEntityConfigurationBase<RegionHoliday>
{
    protected override void DoConfigure(EntityTypeBuilder<RegionHoliday> builder)
    {
    }
}