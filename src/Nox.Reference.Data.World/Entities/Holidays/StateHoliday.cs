using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class StateHoliday : WorldNoxReferenceEntity
{
    public string State { get; private set; } = string.Empty;
    public string StateName { get; private set; } = string.Empty;
    public virtual IReadOnlyList<HolidayData> Holidays { get; private set; } = new List<HolidayData>();
    public virtual IReadOnlyList<RegionHoliday> Regions { get; private set; } = new List<RegionHoliday>();
}