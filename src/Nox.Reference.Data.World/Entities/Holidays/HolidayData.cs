using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class HolidayData : WorldNoxReferenceEntity
{
    public string? Name { get; private set; } = string.Empty;
    public string? Type { get; private set; } = string.Empty;
    public string? Date { get; private set; } = string.Empty;
    public virtual IReadOnlyList<LocalHolidayName> LocalNames { get; private set; } = new List<LocalHolidayName>();
}