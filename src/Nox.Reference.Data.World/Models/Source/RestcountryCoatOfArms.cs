using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Country.DataContext;

public class RestcountryCoatOfArms : ICoatOfArms
{
    [JsonPropertyName("svg")]
    public string Svg { get; set; } = string.Empty;

    [JsonPropertyName("png")]
    public string Png { get; set; } = string.Empty;
}