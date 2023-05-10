using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.Countries;

internal class ContinentConfiguration : NoxReferenceEntityConfigurationBase<Continent>
{
    protected override void DoConfigure(EntityTypeBuilder<Continent> builder)
    {
    }
}