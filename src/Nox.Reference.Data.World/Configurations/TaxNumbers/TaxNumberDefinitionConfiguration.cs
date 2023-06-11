using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.VatNumbers;

internal class TaxNumberDefinitionConfiguration : NoxReferenceEntityConfigurationBase<TaxNumberDefinition>
{
    protected override void DoConfigure(EntityTypeBuilder<TaxNumberDefinition> builder)
    {
        builder
            .HasMany(x => x.ValidationRules)
            .WithOne();

        builder
            .HasOne(x => x.Country)
            .WithOne(x => x.TaxNumberDefinition);
    }
}