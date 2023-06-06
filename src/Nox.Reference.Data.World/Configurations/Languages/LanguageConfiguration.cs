using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations;

internal class LanguageConfiguration : NoxReferenceKeyedEntityConfigurationBase<Language, string>
{
    protected override void DoConfigure(EntityTypeBuilder<Language> builder)
    {
        builder
            .HasMany(x => x.NameTranslations)
            .WithOne();
    }
}