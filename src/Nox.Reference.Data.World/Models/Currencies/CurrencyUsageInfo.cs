using System.Text.Json.Serialization;

namespace Nox.Reference;

public class CurrencyUsageInfo
{
    [JsonPropertyName("frequent")]
    public IReadOnlyList<string> Frequent { get; set; } = new List<string>();

    [JsonPropertyName("rare")]
    public IReadOnlyList<string> Rare { get; set; } = new List<string>();
}