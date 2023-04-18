using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext;

internal class RegionHoliday : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Region { get; set; }
    public string RegionName { get; set; }
    public IReadOnlyList<HolidayData> Holidays { get; set; }
}