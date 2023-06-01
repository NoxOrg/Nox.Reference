using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public abstract class WorldNoxReferenceEntity : INoxReferenceEntity
{
    internal int Id { get; private set; }
    int INoxReferenceEntity.Id => Id;
}