﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Configurations.Currencies;

internal class CurrencyConfiguration : NoxReferenceKeyedEntityConfigurationBase<Currency, string>
{
    protected override void DoConfigure(EntityTypeBuilder<Currency> builder)
    {
        builder
            .HasOne(x => x.MinorUnit)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        builder
           .HasOne(x => x.MajorUnit)
           .WithMany()
           .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Banknotes)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        builder
           .HasOne(x => x.Coins)
           .WithMany()
           .OnDelete(DeleteBehavior.Cascade);
    }
}