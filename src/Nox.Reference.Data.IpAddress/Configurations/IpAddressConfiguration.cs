using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.IpAddress.Configurations;

internal class IpAddressConfiguration : NoxReferenceEntityConfigurationBase<IpAddress>
{
    protected override void DoConfigure(EntityTypeBuilder<IpAddress> builder)
    {
        //builder.OwnsOne(x => x.StartAddress);
        //builder.OwnsOne(x => x.EndAddress);

        //builder.Property(e => e.StartAddress)
        // .HasConversion(
        //     v => new { v.Start, v.End },
        //     v => new IpAddressChunk(v.Start, v.End));

        //builder.Property(e => e.EndAddress)
        // .HasConversion(
        //     v => new { v.Start, v.End },
        //     v => new IpAddressChunk(v.Start, v.End));
    }
}