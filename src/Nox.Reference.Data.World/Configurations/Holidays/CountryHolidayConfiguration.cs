using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations;

internal class CountryHolidayConfiguration : NoxReferenceEntityConfigurationBase<CountryHoliday>
{
    protected override void DoConfigure(EntityTypeBuilder<CountryHoliday> builder)
    {
        builder
            .HasMany(x => x.Holidays)
            .WithOne();

        builder
            .HasMany(x => x.States)
            .WithOne();
    }
}