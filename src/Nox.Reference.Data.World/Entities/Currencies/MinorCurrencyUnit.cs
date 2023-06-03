using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class MinorCurrencyUnit : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Symbol { get; private set; } = string.Empty;
    public decimal MajorValue { get; private set; }
}