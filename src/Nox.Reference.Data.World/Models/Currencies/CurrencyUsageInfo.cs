using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

internal class CurrencyUsageInfo : ICurrencyUsage
{
    [JsonPropertyName("frequent")]
    public IReadOnlyList<string> Frequent { get; set; } = new List<string>();

    [JsonPropertyName("rare")]
    public IReadOnlyList<string> Rare { get; set; } = new List<string>();
}