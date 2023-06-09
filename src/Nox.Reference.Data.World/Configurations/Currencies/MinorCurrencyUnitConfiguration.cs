﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.Currencies;

internal class MinorCurrencyUnitConfiguration : NoxReferenceEntityConfigurationBase<MinorCurrencyUnit>
{
    protected override void DoConfigure(EntityTypeBuilder<MinorCurrencyUnit> builder)
    {
    }
}