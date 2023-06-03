using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class AlternateSpelling : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
}