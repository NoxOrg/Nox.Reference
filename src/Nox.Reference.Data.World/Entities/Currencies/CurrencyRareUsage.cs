using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class CurrencyRareUsage : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string Name { get; set; } = string.Empty;
}