using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.Configurations;

internal class ContinentConfiguration : NoxReferenceEntityConfigurationBase<Continent>
{
    protected override void DoConfigure(EntityTypeBuilder<Continent> builder)
    {
    }
}