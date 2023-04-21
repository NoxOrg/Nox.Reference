using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

internal class CurrencyInfo : ICurrencyInfo
{
    [JsonPropertyName("isoCode")] public string IsoCode { get; set; } = string.Empty;
    [JsonPropertyName("isoNumber")] public string IsoNumber { get; set; } = string.Empty;
    [JsonPropertyName("symbol")] public string Symbol { get; set; } = string.Empty;
    [JsonPropertyName("thousandsSeparator")] public string ThousandsSeparator { get; set; } = string.Empty;
    [JsonPropertyName("decimalSeparator")] public string DecimalSeparator { get; set; } = string.Empty;
    [JsonPropertyName("symbolOnLeft")] public bool SymbolOnLeft { get; set; } = false;
    [JsonPropertyName("spaceBetweenAmountAndSymbol")] public bool SpaceBetweenAmountAndSymbol { get; set; } = false;
    [JsonPropertyName("decimalDigits")] public int DecimalDigits { get; set; } = 0;
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    [JsonPropertyName("units")] public ICurrencyUnit Units { get; set; } = new CurrencyUnitInfo();
    [JsonPropertyName("banknotes")] public ICurrencyUsage Banknotes { get; set; } = new CurrencyUsageInfo();
    [JsonPropertyName("coins")] public ICurrencyUsage Coins { get; set; } = new CurrencyUsageInfo();
}