using Nox.Reference.Abstractions.Cultures;
using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models.Cultures
{
    internal class NumberFormatInfo : INumberFormatInfo
    {
        [JsonPropertyName("currencySymbol")] public string CurrencySymbol_ { get; set; } = string.Empty;
        [JsonPropertyName("decimalSeparator")] public string DecimalSeparator_ { get; set; } = string.Empty;
        [JsonPropertyName("digit")] public string Digit_ { get; set; } = string.Empty;
        [JsonPropertyName("exponentSeparator")] public string ExponentSeparator_ { get; set; } = string.Empty;
        [JsonPropertyName("groupingSeparator")] public string GroupingSeparator_ { get; set; } = string.Empty;
        [JsonPropertyName("infinity")] public string Infinity_ { get; set; } = string.Empty;
        [JsonPropertyName("internationalCurrencySymbol")] public string InternationalCurrencySymbol_ { get; set; } = string.Empty;
        [JsonPropertyName("minusSign")] public string MinusSign_ { get; set; } = string.Empty;
        [JsonPropertyName("monetaryDecimalSeparator")] public string MonetaryDecimalSeparator_ { get; set; } = string.Empty;
        [JsonPropertyName("notANumberSymbol")] public string NotANumberSymbol_ { get; set; } = string.Empty;
        [JsonPropertyName("padEscape")] public string PadEscape_ { get; set; } = string.Empty;
        [JsonPropertyName("patternSeparator")] public string PatternSeparator_ { get; set; } = string.Empty;
        [JsonPropertyName("percent")] public string Percent_ { get; set; } = string.Empty;
        [JsonPropertyName("perMill")] public string PerMill_ { get; set; } = string.Empty;
        [JsonPropertyName("plusSign")] public string PlusSign_ { get; set; } = string.Empty;
        [JsonPropertyName("significantDigit")] public string SignificantDigit_ { get; set; } = string.Empty;
        [JsonPropertyName("zeroDigit")] public string ZeroDigit_ { get; set; } = string.Empty;

        [JsonIgnore] public string CurrencySymbol => CurrencySymbol_;
        [JsonIgnore] public string DecimalSeparator => DecimalSeparator_;
        [JsonIgnore] public string Digit => Digit_;
        [JsonIgnore] public string ExponentSeparator => ExponentSeparator_;
        [JsonIgnore] public string GroupingSeparator => GroupingSeparator_;
        [JsonIgnore] public string Infinity => Infinity_;
        [JsonIgnore] public string InternationalCurrencySymbol => InternationalCurrencySymbol_;
        [JsonIgnore] public string MinusSign => MinusSign_;
        [JsonIgnore] public string MonetaryDecimalSeparator => MonetaryDecimalSeparator_;
        [JsonIgnore] public string NotANumberSymbol => NotANumberSymbol_;
        [JsonIgnore] public string PadEscape => PadEscape_;
        [JsonIgnore] public string PatternSeparator => PatternSeparator_;
        [JsonIgnore] public string Percent => Percent_;
        [JsonIgnore] public string PerMill => PerMill_;
        [JsonIgnore] public string PlusSign => PlusSign_;
        [JsonIgnore] public string SignificantDigit => SignificantDigit_;
        [JsonIgnore] public string ZeroDigit => ZeroDigit_;
    }
}
