using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.Configurations;

internal class CurrencyUsageConfiguration : NoxReferenceEntityConfigurationBase<CurrencyUsage>
{
    protected override void DoConfigure(EntityTypeBuilder<CurrencyUsage> builder)
    {
    }
}