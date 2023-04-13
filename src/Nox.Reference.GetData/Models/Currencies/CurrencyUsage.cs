using System.Text.Json.Serialization;
using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.GetData.Models;

public class CurrencyUsage : ICurrencyUsage
{
    [JsonPropertyName("frequent")] public IReadOnlyList<string> Frequent_ { get; set; } = new List<string>();
    [JsonPropertyName("rare")] public IReadOnlyList<string> Rare_ { get; set; } = new List<string>();

    [JsonIgnore] public IReadOnlyList<string> Frequent => Frequent_;
    [JsonIgnore] public IReadOnlyList<string> Rare => Rare_;
}