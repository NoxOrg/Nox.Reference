using System.Text.Json.Serialization;
using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.Country.DataContext;

internal class MinorCurrencyUnitInfo : IMinorCurrencyUnit
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("symbol")] public string Symbol { get; set; } = "";
    [JsonPropertyName("majorValue")] public decimal MajorValue { get; set; } = 0;
}