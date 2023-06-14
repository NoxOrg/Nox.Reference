using System.Text.Json.Serialization;

namespace Nox.Reference;

public class DialingInfo
{
    [JsonPropertyName("root")]
    public string Prefix { get; set; } = string.Empty;

    [JsonPropertyName("suffixes")]
    public IReadOnlyList<string> Suffixes { get; set; } = new List<string>();
}