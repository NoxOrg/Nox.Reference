using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Entity;

namespace Nox.Reference.Data.Configurations;

public class MacAddressConfiguration : NoxReferenceEntityConfigurationBase<MacAddress>
{
    protected override void DoConfigure(EntityTypeBuilder<MacAddress> builder)
    {
        builder.Property(x => x.IEEERegistry).HasMaxLength(100);
    }
}