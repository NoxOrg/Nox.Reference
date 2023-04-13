namespace Nox.Reference.Data;

public class MajorCurrencyUnit : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string? Name { get; set; }
    public string? Symbol { get; set; }
}
