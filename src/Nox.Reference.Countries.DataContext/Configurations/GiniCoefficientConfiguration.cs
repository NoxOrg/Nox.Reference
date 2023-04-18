using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext.Configurations;

internal class GiniCoefficientConfiguration : NoxReferenceEntityConfigurationBase<GiniCoefficient>
{
    protected override void DoConfigure(EntityTypeBuilder<GiniCoefficient> builder)
    {
    }
}