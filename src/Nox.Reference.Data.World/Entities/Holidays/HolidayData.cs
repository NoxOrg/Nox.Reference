using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class HolidayData : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string? Name { get; private set; } = string.Empty;
    public string? Type { get; private set; } = string.Empty;
    public string? Date { get; private set; } = string.Empty;
    public IReadOnlyList<LocalHolidayName> LocalNames { get; private set; } = Array.Empty<LocalHolidayName>();
}