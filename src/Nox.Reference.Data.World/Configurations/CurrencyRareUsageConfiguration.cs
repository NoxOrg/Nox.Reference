﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations;

internal class CurrencyRareUsageConfiguration : NoxReferenceEntityConfigurationBase<CurrencyRareUsage>
{
    protected override void DoConfigure(EntityTypeBuilder<CurrencyRareUsage> builder)
    {
    }
}