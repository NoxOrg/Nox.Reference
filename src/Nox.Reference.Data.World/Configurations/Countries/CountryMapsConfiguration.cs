using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.Countries;

internal class CountryMapsConfiguration : NoxReferenceEntityConfigurationBase<CountryMaps>
{
    protected override void DoConfigure(EntityTypeBuilder<CountryMaps> builder)
    {
    }
}