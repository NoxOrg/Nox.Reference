using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class CountryHoliday : INoxReferenceEntity
{
    public int Id { get; private set; }
    public int Year { get; set; }
    public string? Country { get; set; } = string.Empty;
    public string? CountryName { get; set; } = string.Empty;
    public string? DayOff { get; set; } = string.Empty;
    public IReadOnlyList<HolidayData> Holidays { get; set; } = new List<HolidayData>();
    public IReadOnlyList<StateHoliday> States { get; set; } = new List<StateHoliday>();
}