using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.Countries;

internal class CountryNativeNameConfiguration : NoxReferenceEntityConfigurationBase<CountryNativeName>
{
    protected override void DoConfigure(EntityTypeBuilder<CountryNativeName> builder)
    {
        builder
            .HasOne(x => x.Language)
            .WithMany();
    }
}