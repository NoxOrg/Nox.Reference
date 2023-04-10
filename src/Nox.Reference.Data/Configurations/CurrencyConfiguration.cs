using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.Configurations;

internal class CurrencyConfiguration : NoxReferenceEntityConfigurationBase<Currency>
{
    protected override void DoConfigure(EntityTypeBuilder<Currency> builder)
    {
        builder
            .HasOne(x => x.Name)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
    }
}