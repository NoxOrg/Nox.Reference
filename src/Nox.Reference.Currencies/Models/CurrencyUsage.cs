
using System.Text.Json.Serialization;

namespace Nox.Reference.Abstractions.Currencies;

public class CurrencyUsage : ICurrencyUsage
{
    [JsonPropertyName("frequent")] public List<string> Frequent_ { get; set; } = new List<string>();
    [JsonPropertyName("rare")] public List<string> Rare_ { get; set; } = new List<string>();
    
    [JsonIgnore] public List<string> Frequent => Frequent_;
    [JsonIgnore] public List<string> Rare => Rare_;
}