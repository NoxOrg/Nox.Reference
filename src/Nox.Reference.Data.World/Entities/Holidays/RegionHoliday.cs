﻿using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class RegionHoliday : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Region { get; set; } = string.Empty;
    public string RegionName { get; set; } = string.Empty;
    public IReadOnlyList<HolidayData> Holidays { get; set; } = new List<HolidayData>();
}