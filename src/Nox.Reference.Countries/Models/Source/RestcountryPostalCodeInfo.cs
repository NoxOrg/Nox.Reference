
using System.Text.Json.Serialization;

namespace Nox.Reference.Countries;

public class RestcountryPostalCodeInfo : IPostalCodeInfo
{
    [JsonPropertyName("format")]
    public string Format { get; set; } = string.Empty;

    [JsonPropertyName("regex")]
    public string Regex { get; set; } = string.Empty;
}
