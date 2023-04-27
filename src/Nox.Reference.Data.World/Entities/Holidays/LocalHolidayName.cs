using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class LocalHolidayName : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string? Name { get; set; } = string.Empty;
    public string? Language { get; set; } = string.Empty;
}