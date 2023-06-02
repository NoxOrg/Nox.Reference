﻿using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class MinorCurrencyUnit : NoxReferenceEntityBase
{
    public string Name { get; private set; } = string.Empty;
    public string Symbol { get; private set; } = string.Empty;
    public decimal MajorValue { get; private set; }
}