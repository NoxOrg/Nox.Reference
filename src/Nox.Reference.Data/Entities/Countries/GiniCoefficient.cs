﻿using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext;

internal class GiniCoefficient : INoxReferenceEntity
{
    public int Id { get; private set; }
    public int Year { get; set; }
    public decimal Value { get; set; }
}