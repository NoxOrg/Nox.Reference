using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Entities.Cultures;

internal class NumberFormat : INoxReferenceEntity
{
    public int Id { get; set; }
    public string CurrencySymbol { get; set; } = string.Empty;
    public string DecimalSeparator { get; set; } = string.Empty;
    public string Digit { get; set; } = string.Empty;
    public string ExponentSeparator { get; set; } = string.Empty;
    public string GroupingSeparator { get; set; } = string.Empty;
    public string Infinity { get; set; } = string.Empty;
    public string InternationalCurrencySymbol { get; set; } = string.Empty;
    public string MinusSign { get; set; } = string.Empty;
    public string MonetaryDecimalSeparator { get; set; } = string.Empty;
    public string NotANumberSymbol { get; set; } = string.Empty;
    public string PadEscape { get; set; } = string.Empty;
    public string PatternSeparator { get; set; } = string.Empty;
    public string Percent { get; set; } = string.Empty;
    public string PerMill { get; set; } = string.Empty;
    public string PlusSign { get; set; } = string.Empty;
    public string SignificantDigit { get; set; } = string.Empty;
    public string ZeroDigit { get; set; } = string.Empty;
    public Culture? Culture { get; set; }
}
