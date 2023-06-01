using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class LocalHolidayName : WorldNoxReferenceEntity
{
    public string? Name { get; private set; } = string.Empty;
    public string? Language { get; private set; } = string.Empty;
}