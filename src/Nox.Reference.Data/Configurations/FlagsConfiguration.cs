using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext.Configurations;

internal class FlagsConfiguration : NoxReferenceEntityConfigurationBase<Flags>
{
    protected override void DoConfigure(EntityTypeBuilder<Flags> builder)
    {
    }
}