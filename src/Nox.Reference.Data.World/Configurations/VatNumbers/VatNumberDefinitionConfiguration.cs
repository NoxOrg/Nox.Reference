using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.VatNumbers;

internal class VatNumberDefinitionConfiguration : NoxReferenceEntityConfigurationBase<VatNumberDefinition>
{
    protected override void DoConfigure(EntityTypeBuilder<VatNumberDefinition> builder)
    {
        builder
            .HasMany(x => x.ValidationRules)
            .WithOne();

        builder
            .HasOne(x => x.Country)
            .WithOne(x => x.VatNumberDefinition);
    }
}