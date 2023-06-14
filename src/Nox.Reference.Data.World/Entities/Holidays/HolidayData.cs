namespace Nox.Reference;

public class HolidayData : NoxReferenceEntityBase
{
    public string? Name { get; private set; } = string.Empty;
    public string? Type { get; private set; } = string.Empty;
    public string? Date { get; private set; } = string.Empty;
    public virtual IReadOnlyList<LocalHolidayName> LocalNames { get; private set; } = new List<LocalHolidayName>();
}