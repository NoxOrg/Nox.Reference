using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext;

internal class CountryHoliday : INoxReferenceEntity
{
    public int Id { get; private set; }
    public int Year { get; private set; }
    public Country Country { get; set; }
    public string DayOff { get; set; }
    public IReadOnlyList<HolidayData> Holidays { get; set; }
    public IReadOnlyList<StateHoliday> States { get; set; }
}