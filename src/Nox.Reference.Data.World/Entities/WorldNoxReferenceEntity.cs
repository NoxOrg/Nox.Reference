using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public abstract class WorldNoxReferenceEntity : INoxReferenceEntity
{
    internal int EntityId { get; private set; }
    int INoxReferenceEntity.EntityId => EntityId;
}