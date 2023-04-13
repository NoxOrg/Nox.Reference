using System.Text.Json.Serialization;
using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.GetData.Models;

public class CurrencyInfo : ICurrencyInfo
{
    [JsonPropertyName("isoCode")] public string IsoCode_ { get; set; } = "";
    [JsonPropertyName("isoNumber")] public string IsoNumber_ { get; set; } = "";
    [JsonPropertyName("symbol")] public string Symbol_ { get; set; } = "";
    [JsonPropertyName("thousandsSeparator")] public string ThousandsSeparator_ { get; set; } = "";
    [JsonPropertyName("decimalSeparator")] public string DecimalSeparator_ { get; set; } = "";
    [JsonPropertyName("symbolOnLeft")] public bool SymbolOnLeft_ { get; set; } = false;
    [JsonPropertyName("spaceBetweenAmountAndSymbol")] public bool SpaceBetweenAmountAndSymbol_ { get; set; } = false;
    [JsonPropertyName("decimalDigits")] public int DecimalDigits_ { get; set; } = 0;
    [JsonPropertyName("name")] public string Name_ { get; set; } = "";
    [JsonPropertyName("units")] public CurrencyUnit Units_ { get; set; } = new CurrencyUnit();
    [JsonPropertyName("banknotes")] public CurrencyUsage Banknotes_ { get; set; } = new CurrencyUsage();
    [JsonPropertyName("coins")] public CurrencyUsage Coins_ { get; set; } = new CurrencyUsage();

    [JsonIgnore] public string IsoCode => IsoCode_;
    [JsonIgnore] public string IsoNumber => IsoNumber_;
    [JsonIgnore] public string Symbol => Symbol_;
    [JsonIgnore] public string ThousandsSeparator => ThousandsSeparator_;
    [JsonIgnore] public string DecimalSeparator => DecimalSeparator_;
    [JsonIgnore] public bool SymbolOnLeft => SymbolOnLeft_;
    [JsonIgnore] public bool SpaceBetweenAmountAndSymbol => SpaceBetweenAmountAndSymbol_;
    [JsonIgnore] public int DecimalDigits => DecimalDigits_;
    [JsonIgnore] public string Name => Name_;
    [JsonIgnore] public ICurrencyUnit Units => Units_;
    [JsonIgnore] public ICurrencyUsage Banknotes => Banknotes_;
    [JsonIgnore] public ICurrencyUsage Coins => Coins_;
}