using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class VatNumberDefinitionConfiguration : NoxReferenceEntityConfigurationBase<VatNumberDefinition>
{
    protected override void DoConfigure(EntityTypeBuilder<VatNumberDefinition> builder)
    {
        builder
            .HasMany(x => x.ValidationRules)
            .WithOne();
    }
}