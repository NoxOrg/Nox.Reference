﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.Configurations;

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