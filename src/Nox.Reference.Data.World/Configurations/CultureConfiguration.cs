using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations;

internal class CultureConfiguration : NoxReferenceEntityConfigurationBase<Culture>
{
    protected override void DoConfigure(EntityTypeBuilder<Culture> builder)
    {
        builder
            .HasOne(x => x.NumberFormat)
            .WithOne(x => x.Culture)
            .HasForeignKey<NumberFormat>("CultureId")
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(x => x.DateFormat)
            .WithOne(x => x.Culture)
            .HasForeignKey<DateFormat>("CultureId")
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(x => x.Country)
            .WithMany(x => x.Cultures);
    }
}