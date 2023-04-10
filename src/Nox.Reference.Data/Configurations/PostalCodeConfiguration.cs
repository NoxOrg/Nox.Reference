using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.Configurations;

internal class PostalCodeConfiguration : NoxReferenceEntityConfigurationBase<PostalCode>
{
    protected override void DoConfigure(EntityTypeBuilder<PostalCode> builder)
    {
    }
}