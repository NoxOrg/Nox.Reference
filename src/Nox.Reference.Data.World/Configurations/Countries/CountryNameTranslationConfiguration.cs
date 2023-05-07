using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.Countries;

internal class CountryNameTranslationConfiguration : NoxReferenceEntityConfigurationBase<CountryNameTranslation>
{
    protected override void DoConfigure(EntityTypeBuilder<CountryNameTranslation> builder)
    {
        builder
            .HasOne(x => x.Language)
            .WithMany();
    }
}