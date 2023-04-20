using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext.Configurations;

internal class CurrencyUsageConfiguration : NoxReferenceEntityConfigurationBase<CurrencyUsage>
{
    protected override void DoConfigure(EntityTypeBuilder<CurrencyUsage> builder)
    {
        builder
           .HasMany(x => x.Frequent)
           .WithOne()
           .OnDelete(DeleteBehavior.Cascade);

        builder
          .HasMany(x => x.Rare)
          .WithOne()
          .OnDelete(DeleteBehavior.Cascade);
    }
}