using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext.Configurations;

internal class CurrencyFrequentUsageConfiguration : NoxReferenceEntityConfigurationBase<CurrencyFrequentUsage>
{
    protected override void DoConfigure(EntityTypeBuilder<CurrencyFrequentUsage> builder)
    {
    }
}