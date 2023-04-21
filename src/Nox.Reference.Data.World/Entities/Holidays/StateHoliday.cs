using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class StateHoliday : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string State { get; set; } = string.Empty;
    public string StateName { get; set; } = string.Empty;
    public IReadOnlyList<HolidayData> Holidays { get; set; } = new List<HolidayData>();
    public IReadOnlyList<RegionHoliday> Regions { get; set; } = new List<RegionHoliday>();
}