using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.Data;

internal class CurrencyInfo : ICurrencyInfo
{
    public string IsoCode { get; private set; }
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
    public ICurrencyUnit Units { get; }
}