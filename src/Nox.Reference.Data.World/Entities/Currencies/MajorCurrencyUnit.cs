using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class MajorCurrencyUnit : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Name { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
}