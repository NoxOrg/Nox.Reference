using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.Machine;

internal class MacAddressConfiguration : NoxReferenceEntityConfigurationBase<MacAddress>
{
    protected override void DoConfigure(EntityTypeBuilder<MacAddress> builder)
    {
    }
}