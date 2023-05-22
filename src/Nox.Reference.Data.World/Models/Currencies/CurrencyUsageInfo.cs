using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

internal class CurrencyUsageInfo
{
    [JsonPropertyName("frequent")]
    public IReadOnlyList<string> Frequent { get; set; } = Array.Empty<string>();

    [JsonPropertyName("rare")]
    public IReadOnlyList<string> Rare { get; set; } = Array.Empty<string>();
}