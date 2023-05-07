using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.Countries;

internal class CountryVehicleConfiguration : NoxReferenceEntityConfigurationBase<CountryVehicle>
{
    protected override void DoConfigure(EntityTypeBuilder<CountryVehicle> builder)
    {
    }
}