using Nox.Reference.Data.Common;

namespace Nox.Reference.Data;

public abstract class MachineNoxReferenceEntity : INoxReferenceEntity
{
    internal int EntityId { get; private set; }
    int INoxReferenceEntity.EntityId => EntityId;
}