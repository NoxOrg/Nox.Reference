using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.Configurations;

internal class GiniCoefficientConfiguration : NoxReferenceEntityConfigurationBase<GiniCoefficient>
{
    protected override void DoConfigure(EntityTypeBuilder<GiniCoefficient> builder)
    {
    }
}