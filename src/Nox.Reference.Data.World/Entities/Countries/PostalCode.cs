using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class PostalCode : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string? Format { get; private set; } = string.Empty;
    public string? Regex { get; private set; } = string.Empty;
}