using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.Configurations;

internal class CurrencyFrequentUsageConfiguration : NoxReferenceEntityConfigurationBase<CurrencyFrequentUsage>
{
    protected override void DoConfigure(EntityTypeBuilder<CurrencyFrequentUsage> builder)
    {
    }
}