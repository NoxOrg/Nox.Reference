using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext.Configurations;

internal class MinorCurrencyUnitConfiguration : NoxReferenceEntityConfigurationBase<MinorCurrencyUnit>
{
    protected override void DoConfigure(EntityTypeBuilder<MinorCurrencyUnit> builder)
    {
    }
}