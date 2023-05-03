using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.Countries;

internal class CountryNamesConfiguration : NoxReferenceEntityConfigurationBase<CountryNames>
{
    protected override void DoConfigure(EntityTypeBuilder<CountryNames> builder)
    {
        builder
            .HasMany(x => x.NativeNames)
            .WithOne();
    }
}