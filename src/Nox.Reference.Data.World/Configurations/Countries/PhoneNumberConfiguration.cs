using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.Countries;

internal class PhoneNumberConfiguration : NoxReferenceEntityConfigurationBase<PhoneNumber>
{
    protected override void DoConfigure(EntityTypeBuilder<PhoneNumber> builder)
    {
    }
}