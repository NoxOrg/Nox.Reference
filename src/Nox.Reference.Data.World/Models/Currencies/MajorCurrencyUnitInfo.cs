using System.Text.Json.Serialization;

namespace Nox.Reference;

public class MajorCurrencyUnitInfo
{
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    [JsonPropertyName("symbol")] public string Symbol { get; set; } = string.Empty;
}