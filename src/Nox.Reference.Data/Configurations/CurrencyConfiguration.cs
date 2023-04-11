﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nox.Reference.Data.Configurations;

internal class CurrencyConfiguration : NoxReferenceEntityConfigurationBase<Currency>
{
    protected override void DoConfigure(EntityTypeBuilder<Currency> builder)
    {
        builder
            .HasOne(x => x.Banknotes)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Coins)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Units)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
    }
}