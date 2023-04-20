using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext;

internal class Currency : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string IsoCode { get; set; }
    public string IsoNumber { get; set; }
    public string Symbol { get; set; }
    public string ThousandsSeparator { get; set; }
    public string DecimalSeparator { get; set; }
    public bool SymbolOnLeft { get; set; }
    public bool SpaceBetweenAmountAndSymbol { get; set; }
    public int DecimalDigits { get; set; }
    public string Name { get; set; }
    public CurrencyUsage Banknotes { get; set; }
    public CurrencyUsage Coins { get; set; }
    public MajorCurrencyUnit? MajorUnit { get; set; }
    public MinorCurrencyUnit? MinorUnit { get; set; }
}