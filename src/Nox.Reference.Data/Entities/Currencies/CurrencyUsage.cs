namespace Nox.Reference.Data;

internal class CurrencyUsage : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string? Frequent { get; set; }
    public string? Rare { get; set; }
}