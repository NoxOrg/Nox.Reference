using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations;

internal class StateHolidayConfiguration : NoxReferenceEntityConfigurationBase<StateHoliday>
{
    protected override void DoConfigure(EntityTypeBuilder<StateHoliday> builder)
    {
        builder.HasMany(x => x.Holidays)
            .WithOne();

        builder.HasMany(x => x.Regions)
           .WithOne();
    }
}