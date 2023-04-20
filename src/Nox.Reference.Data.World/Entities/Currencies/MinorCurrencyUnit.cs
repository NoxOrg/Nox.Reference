using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class MinorCurrencyUnit : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string? Name { get; set; }
    public string? Symbol { get; set; }
    public decimal MajorValue { get; set; }
}