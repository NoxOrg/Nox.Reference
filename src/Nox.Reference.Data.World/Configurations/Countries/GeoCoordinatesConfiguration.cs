using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.Countries;

internal class GeoCoordinatesConfiguration : NoxReferenceEntityConfigurationBase<GeoCoordinates>
{
    protected override void DoConfigure(EntityTypeBuilder<GeoCoordinates> builder)
    {
    }
}
