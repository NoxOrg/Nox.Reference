using Nox.Reference.Data.Common;

namespace Nox.Reference.Data;

public abstract class MachineNoxReferenceEntity : INoxReferenceEntity
{
    internal int Id { get; private set; }
    int INoxReferenceEntity.Id => Id;
}
