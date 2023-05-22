using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class GiniCoefficient : INoxReferenceEntity
{
    public int Id { get; private set; }
    public int Year { get; internal set; }
    public decimal Value { get; internal set; }
}