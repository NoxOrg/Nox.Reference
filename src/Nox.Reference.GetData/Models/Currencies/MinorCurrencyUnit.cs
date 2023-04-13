using System.Text.Json.Serialization;
using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.GetData.Models;

public class MinorCurrencyUnit : IMinorCurrencyUnit
{
    [JsonPropertyName("name")] public string Name_ { get; set; } = "";
    [JsonPropertyName("symbol")] public string Symbol_ { get; set; } = "";
    [JsonPropertyName("majorValue")] public decimal MajorValue_ { get; set; } = 0;

    [JsonIgnore] public string Name => Name_;
    [JsonIgnore] public string Symbol => Symbol_;
    [JsonIgnore] public decimal MajorValue => MajorValue_;
}