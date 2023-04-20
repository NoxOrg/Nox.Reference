using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class Currency : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string IsoCode { get; set; } = string.Empty;
    public string IsoNumber { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public string ThousandsSeparator { get; set; } = string.Empty;
    public string DecimalSeparator { get; set; } = string.Empty;
    public bool SymbolOnLeft { get; set; }
    public bool SpaceBetweenAmountAndSymbol { get; set; }
    public int DecimalDigits { get; set; }
    public string Name { get; set; } = string.Empty;
    public CurrencyUsage Banknotes { get; set; } = new CurrencyUsage();
    public CurrencyUsage Coins { get; set; } = new CurrencyUsage();
    public MajorCurrencyUnit MajorUnit { get; set; } = new MajorCurrencyUnit();
    public MinorCurrencyUnit MinorUnit { get; set; } = new MinorCurrencyUnit();
}