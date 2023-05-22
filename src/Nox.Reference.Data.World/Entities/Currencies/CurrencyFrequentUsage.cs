﻿using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class CurrencyFrequentUsage : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Name { get; internal set; } = string.Empty;
}