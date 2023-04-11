using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.Configurations;

internal class CurrencyUnitConfiguration : NoxReferenceEntityConfigurationBase<CurrencyUnit>
{
    protected override void DoConfigure(EntityTypeBuilder<CurrencyUnit> builder)
    {
    }
}