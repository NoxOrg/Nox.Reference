using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Entities;

namespace Nox.Reference.Data.Configurations;

internal class MacAddressConfiguration : NoxReferenceEntityConfigurationBase<MacAddress>
{
    protected override void DoConfigure(EntityTypeBuilder<MacAddress> builder)
    {
        builder.Property(x => x.IEEERegistry).HasMaxLength(100);
    }
}