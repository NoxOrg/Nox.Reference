using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.Countries;

internal class PostalCodeConfiguration : NoxReferenceEntityConfigurationBase<PostalCode>
{
    protected override void DoConfigure(EntityTypeBuilder<PostalCode> builder)
    {
    }
}