namespace Nox.Reference.Data;

internal class CurrencyUnit : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string? MajorName { get; set; }
    public string? MinorName { get; set; }
    public string? MajorSymbol { get; set; }
    public string? MinorSymbol { get; set; }
    public string? MajorValue { get; set; }
    public string? MinorValue { get; set; }
}