using System.Text.Json.Serialization;

namespace Nox.Reference.Countries;

public class RestcountryCoatOfArms : ICoatOfArms
{
    [JsonPropertyName("svg")]
    public string Svg { get; set; } = string.Empty;

    [JsonPropertyName("png")]
    public string Png { get; set; } = string.Empty;
}
