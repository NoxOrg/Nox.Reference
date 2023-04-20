using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class HolidayData : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
}