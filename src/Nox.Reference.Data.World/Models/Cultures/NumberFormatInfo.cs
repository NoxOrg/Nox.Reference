using System.Text.Json.Serialization;

namespace Nox.Reference;

public class NumberFormatInfo
{
    [JsonPropertyName("currencySymbol")] public string CurrencySymbol { get; set; } = string.Empty;
    [JsonPropertyName("decimalSeparator")] public string DecimalSeparator { get; set; } = string.Empty;
    [JsonPropertyName("digit")] public string Digit { get; set; } = string.Empty;
    [JsonPropertyName("exponentSeparator")] public string ExponentSeparator { get; set; } = string.Empty;
    [JsonPropertyName("groupingSeparator")] public string GroupingSeparator { get; set; } = string.Empty;
    [JsonPropertyName("infinity")] public string Infinity { get; set; } = string.Empty;
    [JsonPropertyName("internationalCurrencySymbol")] public string InternationalCurrencySymbol { get; set; } = string.Empty;
    [JsonPropertyName("minusSign")] public string MinusSign { get; set; } = string.Empty;
    [JsonPropertyName("monetaryDecimalSeparator")] public string MonetaryDecimalSeparator { get; set; } = string.Empty;
    [JsonPropertyName("notANumberSymbol")] public string NotANumberSymbol { get; set; } = string.Empty;
    [JsonPropertyName("padEscape")] public string PadEscape { get; set; } = string.Empty;
    [JsonPropertyName("patternSeparator")] public string PatternSeparator { get; set; } = string.Empty;
    [JsonPropertyName("percent")] public string Percent { get; set; } = string.Empty;
    [JsonPropertyName("perMill")] public string PerMill { get; set; } = string.Empty;
    [JsonPropertyName("plusSign")] public string PlusSign { get; set; } = string.Empty;
    [JsonPropertyName("significantDigit")] public string SignificantDigit { get; set; } = string.Empty;
    [JsonPropertyName("zeroDigit")] public string ZeroDigit { get; set; } = string.Empty;
}