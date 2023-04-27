using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.Countries;

internal class TopLevelDomainConfiguration : NoxReferenceEntityConfigurationBase<TopLevelDomain>
{
    protected override void DoConfigure(EntityTypeBuilder<TopLevelDomain> builder)
    {
    }
}