using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class Currency : NoxReferenceEntityBase
{
    public string IsoCode { get; private set; } = string.Empty;
    public string IsoNumber { get; private set; } = string.Empty;
    public string Symbol { get; private set; } = string.Empty;
    public string ThousandsSeparator { get; private set; } = string.Empty;
    public string DecimalSeparator { get; private set; } = string.Empty;
    public bool SymbolOnLeft { get; private set; }
    public bool SpaceBetweenAmountAndSymbol { get; private set; }
    public int DecimalDigits { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public virtual CurrencyUsage Banknotes { get; private set; } = new CurrencyUsage();
    public virtual CurrencyUsage Coins { get; private set; } = new CurrencyUsage();
    public virtual MajorCurrencyUnit MajorUnit { get; private set; } = new MajorCurrencyUnit();
    public virtual MinorCurrencyUnit MinorUnit { get; private set; } = new MinorCurrencyUnit();
}