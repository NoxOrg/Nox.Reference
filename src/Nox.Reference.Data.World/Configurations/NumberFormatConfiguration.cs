﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;
using Nox.Reference.Data.World.Entities.Cultures;

namespace Nox.Reference.Data.World.Configurations;

internal class NumberFormatConfiguration : NoxReferenceEntityConfigurationBase<NumberFormat>
{
    protected override void DoConfigure(EntityTypeBuilder<NumberFormat> builder) { }
}