using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class CountryHoliday : INoxReferenceEntity
{
    public int Id { get; private set; }
    public int Year { get; private set; }
    public Country Country { get; set; } = new Country();
    public string DayOff { get; set; } = string.Empty;
    public IReadOnlyList<HolidayData> Holidays { get; set; } = new List<HolidayData>();
    public IReadOnlyList<StateHoliday> States { get; set; } = new List<StateHoliday>();
}