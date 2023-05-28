using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class CurrencyUsage : INoxReferenceEntity
{
    public int Id { get; private set; }
    public virtual IReadOnlyList<CurrencyFrequentUsage> Frequent { get; private set; } = new List<CurrencyFrequentUsage>();
    public virtual IReadOnlyList<CurrencyRareUsage> Rare { get; private set; } = new List<CurrencyRareUsage>();
}