using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext.Configurations;

internal class PostalCodeConfiguration : NoxReferenceEntityConfigurationBase<PostalCode>
{
    protected override void DoConfigure(EntityTypeBuilder<PostalCode> builder)
    {
    }
}