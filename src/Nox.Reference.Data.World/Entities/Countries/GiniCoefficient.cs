using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class GiniCoefficient : WorldNoxReferenceEntity
{
    public int Year { get; internal set; }
    public decimal Value { get; internal set; }
}