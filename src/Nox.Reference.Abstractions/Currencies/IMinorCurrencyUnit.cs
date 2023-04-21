namespace Nox.Reference.Abstractions;

public interface IMinorCurrencyUnit
{
    public string Name { get; }
    public string Symbol { get; }
    public decimal MajorValue { get; }
}   