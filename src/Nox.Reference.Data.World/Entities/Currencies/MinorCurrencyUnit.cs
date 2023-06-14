namespace Nox.Reference;

public class MinorCurrencyUnit : NoxReferenceEntityBase
{
    public string Name { get; private set; } = string.Empty;
    public string Symbol { get; private set; } = string.Empty;
    public decimal MajorValue { get; private set; }
}