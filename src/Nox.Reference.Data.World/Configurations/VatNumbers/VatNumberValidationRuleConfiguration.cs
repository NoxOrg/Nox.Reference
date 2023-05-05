using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.VatNumbers;

internal class VatNumberValidationRuleConfiguration : NoxReferenceEntityConfigurationBase<VatNumberValidationRule>
{
    protected override void DoConfigure(EntityTypeBuilder<VatNumberValidationRule> builder)
    {
        builder.OwnsOne(x => x.Checksum);
    }
}