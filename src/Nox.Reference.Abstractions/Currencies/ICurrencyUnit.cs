namespace Nox.Reference.Abstractions.Currencies;

public interface ICurrencyUnit
{
    public IMajorCurrencyUnit MajorCurrencyUnit { get; }
    public IMinorCurrencyUnit MinorCurrencyUnit { get; }
}