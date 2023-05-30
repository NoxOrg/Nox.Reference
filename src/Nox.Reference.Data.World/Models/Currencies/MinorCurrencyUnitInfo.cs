using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

public class MinorCurrencyUnitInfo
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("symbol")] public string Symbol { get; set; } = "";
    [JsonPropertyName("majorValue")] public decimal MajorValue { get; set; } = 0;
}