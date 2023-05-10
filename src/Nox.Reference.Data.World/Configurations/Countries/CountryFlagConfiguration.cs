using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.Countries;

internal class CountryFlagConfiguration : NoxReferenceEntityConfigurationBase<CountryFlag>
{
    protected override void DoConfigure(EntityTypeBuilder<CountryFlag> builder)
    {
    }
}