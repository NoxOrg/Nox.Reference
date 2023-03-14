
using System.Text.Json.Serialization;

namespace Nox.Reference.Countries;

public class RestcountryDialingInfo : IDialingInfo
{
    [JsonPropertyName("root")]
    public string Prefix { get; set; } = string.Empty;

    [JsonPropertyName("suffixes")]
    public string[] Suffixes { get; set; } = Array.Empty<string>();
}
