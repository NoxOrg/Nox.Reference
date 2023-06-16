using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Nox.Reference.Data.IpAddress.Configurations;

internal class IpAddressConfiguration : NoxReferenceEntityConfigurationBase<IpAddress>
{
    protected override void DoConfigure(EntityTypeBuilder<IpAddress> builder)
    {
        builder.Property(x => x.CountryCode)
            .IsRequired()
            .HasMaxLength(2);

        builder.OwnsOne(x => x.StartAddress,x=>x.Ignore(t=>t.Address));
        builder.OwnsOne(x => x.EndAddress, x => x.Ignore(t => t.Address));

        builder.Ignore(x => x.StartIpAddress);
        builder.Ignore(x => x.EndIpAddress);
    }
}

//internal class ParentConfiguration : NoxReferenceEntityConfigurationBase<Parent>
//{
//    protected override void DoConfigure(EntityTypeBuilder<Parent> builder)
//    {
//        builder.OwnsOne(x => x.Child1, x => x.Property(x => x.Value).HasConversion<byte[]>());
//        builder.OwnsOne(x => x.Child2, x => x.Property(x => x.Value).HasConversion<byte[]>());
//    }
//}