
using System.Text.Json.Serialization;

namespace Nox.Reference.Abstractions.Currencies;

public class CurrencyUsage : ICurrencyUsage
{
    [JsonPropertyName("frequent")] public IReadOnlyList<string> Frequent_ { get; set; } = new List<string>();
    [JsonPropertyName("rare")] public IReadOnlyList<string> Rare_ { get; set; } = new List<string>();
    
    [JsonIgnore] public IReadOnlyList<string> Frequent => Frequent_;
    [JsonIgnore] public IReadOnlyList<string> Rare => Rare_;
}