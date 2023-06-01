using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class CurrencyUsage : WorldNoxReferenceEntity
{
    public virtual IReadOnlyList<CurrencyFrequentUsage> Frequent { get; private set; } = new List<CurrencyFrequentUsage>();
    public virtual IReadOnlyList<CurrencyRareUsage> Rare { get; private set; } = new List<CurrencyRareUsage>();
}