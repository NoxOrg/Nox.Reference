using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.Configurations;

internal class FlagsConfiguration : NoxReferenceEntityConfigurationBase<Flags>
{
    protected override void DoConfigure(EntityTypeBuilder<Flags> builder)
    {
    }
}