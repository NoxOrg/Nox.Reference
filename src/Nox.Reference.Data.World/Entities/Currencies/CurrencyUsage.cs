using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class CurrencyUsage : INoxReferenceEntity
{
    public int Id { get; private set; }
    public IReadOnlyList<CurrencyFrequentUsage> Frequent { get; private set; } = new List<CurrencyFrequentUsage>();
    public IReadOnlyList<CurrencyRareUsage> Rare { get; private set; } = new List<CurrencyRareUsage>();
}