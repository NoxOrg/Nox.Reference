using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.Configurations;

internal class HolidayDataConfiguration : NoxReferenceEntityConfigurationBase<HolidayData>
{
    protected override void DoConfigure(EntityTypeBuilder<HolidayData> builder)
    {
    }
}