﻿using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class HolidayData : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Date { get; set; }
}