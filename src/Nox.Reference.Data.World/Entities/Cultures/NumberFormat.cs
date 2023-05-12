using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class NumberFormat : INoxReferenceEntity
{
    public int Id { get; set; }
    public string CurrencySymbol { get; private set; } = string.Empty;
    public string DecimalSeparator { get; private set; } = string.Empty;
    public string Digit { get; private set; } = string.Empty;
    public string ExponentSeparator { get; private set; } = string.Empty;
    public string GroupingSeparator { get; private set; } = string.Empty;
    public string Infinity { get; private set; } = string.Empty;
    public string InternationalCurrencySymbol { get; private set; } = string.Empty;
    public string MinusSign { get; private set; } = string.Empty;
    public string MonetaryDecimalSeparator { get; private set; } = string.Empty;
    public string NotANumberSymbol { get; private set; } = string.Empty;
    public string PadEscape { get; private set; } = string.Empty;
    public string PatternSeparator { get; private set; } = string.Empty;
    public string Percent { get; private set; } = string.Empty;
    public string PerMill { get; private set; } = string.Empty;
    public string PlusSign { get; private set; } = string.Empty;
    public string SignificantDigit { get; private set; } = string.Empty;
    public string ZeroDigit { get; private set; } = string.Empty;
    public Culture Culture { get; private set; } = new Culture();
}