using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

public class PostalCodeInfo
{
    [JsonPropertyName("format")]
    public string Format { get; set; } = string.Empty;

    [JsonPropertyName("regex")]
    public string Regex { get; set; } = string.Empty;
}