using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class LocalHolidayName : NoxReferenceEntityBase
{
    public string? Name { get; private set; } = string.Empty;
    public string? Language { get; private set; } = string.Empty;
}