namespace Nox.Reference.Data;

internal class CurrencyUsage : INoxReferenceEntity
{
    public int Id { get; private set; }
    public IReadOnlyList<CurrencyFrequentUsage>? Frequent { get; set; }
    public IReadOnlyList<CurrencyRareUsage>? Rare { get; set; }
}