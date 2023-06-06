using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class CountryHoliday : NoxReferenceEntityBase
{
    public int Year { get; private set; }
    public virtual Country Country { get; private set; } = new Country();
    public string? CountryName { get; private set; } = string.Empty;
    public string? DayOff { get; private set; } = string.Empty;
    public virtual IReadOnlyList<HolidayData> Holidays { get; private set; } = new List<HolidayData>();
    public virtual IReadOnlyList<StateHoliday> States { get; private set; } = new List<StateHoliday>();
}