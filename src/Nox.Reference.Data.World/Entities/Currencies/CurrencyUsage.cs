﻿using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class CurrencyUsage : INoxReferenceEntity
{
    public int Id { get; private set; }
    public IReadOnlyList<CurrencyFrequentUsage> Frequent { get; set; } = new List<CurrencyFrequentUsage>();
    public IReadOnlyList<CurrencyRareUsage> Rare { get; set; } = new List<CurrencyRareUsage>();
}