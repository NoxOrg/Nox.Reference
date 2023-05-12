using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class CountryHoliday : INoxReferenceEntity
{
    public int Id { get; private set; }
    public int Year { get; private set; }
    public string? Country { get; private set; } = string.Empty;
    public string? CountryName { get; private set; } = string.Empty;
    public string? DayOff { get; private set; } = string.Empty;
    public IReadOnlyList<HolidayData> Holidays { get; private set; } = Array.Empty<HolidayData>();
    public IReadOnlyList<StateHoliday> States { get; private set; } = Array.Empty<StateHoliday>();
}