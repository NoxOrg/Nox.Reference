﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations;

internal class LanguageTranslationConfiguration : NoxReferenceEntityConfigurationBase<LanguageTranslation>
{
    protected override void DoConfigure(EntityTypeBuilder<LanguageTranslation> builder)
    {
    }
}