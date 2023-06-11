using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.VatNumbers;

internal class TaxNumberValidationRuleConfiguration : NoxReferenceEntityConfigurationBase<TaxNumberValidationRule>
{
    protected override void DoConfigure(EntityTypeBuilder<TaxNumberValidationRule> builder)
    {
        builder.OwnsOne(x => x.Checksum);
    }
}