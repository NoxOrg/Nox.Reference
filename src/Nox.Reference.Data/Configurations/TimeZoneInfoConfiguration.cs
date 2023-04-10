using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.Configurations;

internal class TimeZoneInfoConfiguration : NoxReferenceEntityConfigurationBase<TimeZoneInfo>
{
    protected override void DoConfigure(EntityTypeBuilder<TimeZoneInfo> builder)
    {
    }
}