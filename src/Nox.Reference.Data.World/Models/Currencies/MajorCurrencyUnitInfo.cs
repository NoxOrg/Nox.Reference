using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Country.DataContext;

internal class MajorCurrencyUnitInfo : IMajorCurrencyUnit
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("symbol")] public string Symbol { get; set; } = "";
}