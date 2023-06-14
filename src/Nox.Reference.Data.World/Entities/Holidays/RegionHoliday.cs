namespace Nox.Reference;

public class RegionHoliday : NoxReferenceEntityBase
{
    public string Region { get; set; } = string.Empty;
    public string RegionName { get; set; } = string.Empty;
    public virtual IReadOnlyList<HolidayData> Holidays { get; private set; } = new List<HolidayData>();
}