using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.IpAddress.Configurations;

internal class IpAddressConfiguration : NoxReferenceEntityConfigurationBase<IpAddress>
{
    private const int CountryCodeLength = 2;

    protected override void DoConfigure(EntityTypeBuilder<IpAddress> builder)
    {
        builder.Property(x => x.CountryCode)
            .IsRequired()
            .HasMaxLength(CountryCodeLength);

        builder.OwnsOne(x => x.StartAddress);
        builder.OwnsOne(x => x.EndAddress);
    }
}