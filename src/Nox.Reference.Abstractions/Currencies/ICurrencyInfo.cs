
namespace Nox.Reference.Abstractions.Currencies;

public interface ICurrencyInfo
{
    public string IsoCode { get; }
    public string IsoNumber { get; }
    public string Symbol { get; }
    public string ThousandsSeparator { get; }
    public string DecimalSeparator { get; }
    public bool SymbolOnLeft { get; }
    public bool SpaceBetweenAmountAndSymbol { get; }
    public int DecimalDigits { get; }
    public string Name { get; }
    public ICurrencyUsage Banknotes { get; }
    public ICurrencyUsage Coins { get; }
}