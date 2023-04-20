using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class Continent : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Name { get; set; }
}