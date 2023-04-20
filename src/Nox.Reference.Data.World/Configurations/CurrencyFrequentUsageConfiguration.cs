using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations;

internal class CurrencyFrequentUsageConfiguration : NoxReferenceEntityConfigurationBase<CurrencyFrequentUsage>
{
    protected override void DoConfigure(EntityTypeBuilder<CurrencyFrequentUsage> builder)
    {
    }
}