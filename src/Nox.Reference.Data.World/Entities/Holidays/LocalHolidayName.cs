using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class LocalHolidayName : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string? Name { get; private set; } = string.Empty;
    public string? Language { get; private set; } = string.Empty;
}