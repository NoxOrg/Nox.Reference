using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.Countries;

internal class CountryDialingConfiguration : NoxReferenceEntityConfigurationBase<CountryDialing>
{
    protected override void DoConfigure(EntityTypeBuilder<CountryDialing> builder)
    {
    }
}
