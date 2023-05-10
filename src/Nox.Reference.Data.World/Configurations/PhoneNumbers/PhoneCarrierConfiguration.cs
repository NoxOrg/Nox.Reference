using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations;

internal class PhoneCarrierConfiguration : NoxReferenceEntityConfigurationBase<PhoneCarrier>
{
    protected override void DoConfigure(EntityTypeBuilder<PhoneCarrier> builder)
    {
        builder.HasMany(x => x.PhoneNumberCarriers)
            .WithOne(x => x.PhoneCarrier);
    }
}