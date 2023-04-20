using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Country.DataContext;

public class RestcountryFlags : IFlags
{
    [JsonPropertyName("svg")]
    public string Svg { get; set; } = string.Empty;

    [JsonPropertyName("png")]
    public string Png { get; set; } = string.Empty;

    [JsonPropertyName("alt")]
    public string AlternateText { get; set; } = string.Empty;
}