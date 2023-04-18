using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext;

internal class CityConfiguration : NoxReferenceEntityConfigurationBase<City>
{
    protected override void DoConfigure(EntityTypeBuilder<City> builder)
    {
        builder
            .HasMany(x => x.PostalCodes)
            .WithOne();
    }
}