using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.Configurations;

internal class CityConfiguration : NoxReferenceEntityConfigurationBase<City>
{
    protected override void DoConfigure(EntityTypeBuilder<City> builder)
    {
        builder
            .HasMany(x => x.PostalCodes)
            .WithOne();
    }
}