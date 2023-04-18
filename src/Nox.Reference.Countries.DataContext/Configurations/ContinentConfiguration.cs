using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext.Configurations;

internal class ContinentConfiguration : NoxReferenceEntityConfigurationBase<Continent>
{
    protected override void DoConfigure(EntityTypeBuilder<Continent> builder)
    {
    }
}