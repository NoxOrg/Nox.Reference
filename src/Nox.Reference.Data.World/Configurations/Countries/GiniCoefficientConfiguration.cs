using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.Countries;

internal class GiniCoefficientConfiguration : NoxReferenceEntityConfigurationBase<GiniCoefficient>
{
    protected override void DoConfigure(EntityTypeBuilder<GiniCoefficient> builder)
    {
    }
}