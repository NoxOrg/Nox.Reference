using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext.Configurations;

internal class TopLevelDomainConfiguration : NoxReferenceEntityConfigurationBase<TopLevelDomain>
{
    protected override void DoConfigure(EntityTypeBuilder<TopLevelDomain> builder)
    {
    }
}