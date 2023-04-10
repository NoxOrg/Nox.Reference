using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.Configurations;

internal class MacAddressConfiguration : NoxReferenceEntityConfigurationBase<MacAddress>
{
    protected override void DoConfigure(EntityTypeBuilder<MacAddress> builder)
    {
    }
}