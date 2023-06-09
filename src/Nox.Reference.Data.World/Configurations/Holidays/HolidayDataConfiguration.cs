﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations;

internal class HolidayDataConfiguration : NoxReferenceEntityConfigurationBase<HolidayData>
{
    protected override void DoConfigure(EntityTypeBuilder<HolidayData> builder)
    {
        builder.HasMany(x => x.LocalNames)
            .WithOne();
    }
}