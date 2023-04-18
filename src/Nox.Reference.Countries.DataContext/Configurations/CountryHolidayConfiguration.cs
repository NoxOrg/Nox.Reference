using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext.Configurations;

internal class CountryHolidayConfiguration : NoxReferenceEntityConfigurationBase<CountryHoliday>
{
    protected override void DoConfigure(EntityTypeBuilder<CountryHoliday> builder)
    {
        builder
            .HasMany(x => x.Holidays)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}