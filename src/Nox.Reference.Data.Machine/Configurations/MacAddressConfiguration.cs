using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.Machine;

internal class MacAddressConfiguration : NoxReferenceKeyedEntityConfigurationBase<MacAddress, string>
{
    protected override void DoConfigure(EntityTypeBuilder<MacAddress> builder)
    {
    }
}