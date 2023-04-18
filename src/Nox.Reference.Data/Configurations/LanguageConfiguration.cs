using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext.Configurations;

internal class LanguageConfiguration : NoxReferenceEntityConfigurationBase<Language>
{
    protected override void DoConfigure(EntityTypeBuilder<Language> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired();
    }
}