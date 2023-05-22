﻿using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class StateHoliday : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string State { get; private set; } = string.Empty;
    public string StateName { get; private set; } = string.Empty;
    public IReadOnlyList<HolidayData> Holidays { get; private set; } = new List<HolidayData>();
    public IReadOnlyList<RegionHoliday> Regions { get; private set; } = new List<RegionHoliday>();
}