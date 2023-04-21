using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations;

internal class TimeZoneInfoConfiguration : NoxReferenceEntityConfigurationBase<TimeZoneInfo>
{
    protected override void DoConfigure(EntityTypeBuilder<TimeZoneInfo> builder)
    {
    }
}