using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.Configurations;

internal class PhoneNumberConfiguration : NoxReferenceEntityConfigurationBase<PhoneNumber>
{
    protected override void DoConfigure(EntityTypeBuilder<PhoneNumber> builder)
    {
    }
}