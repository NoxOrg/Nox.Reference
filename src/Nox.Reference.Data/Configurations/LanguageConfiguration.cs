using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.Configurations;

internal class LanguageConfiguration : NoxReferenceEntityConfigurationBase<Language>
{
    protected override void DoConfigure(EntityTypeBuilder<Language> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired();
    }
}