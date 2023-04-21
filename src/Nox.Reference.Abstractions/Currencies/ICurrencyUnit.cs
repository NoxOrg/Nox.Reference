namespace Nox.Reference.Abstractions;

public interface ICurrencyUnit
{
    public IMajorCurrencyUnit MajorCurrencyUnit { get; }
    public IMinorCurrencyUnit MinorCurrencyUnit { get; }
}