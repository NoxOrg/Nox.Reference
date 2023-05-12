using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

public class DemonymnInfo
{
    [JsonIgnore]
    public string Language { get; set; } = string.Empty;

    [JsonPropertyName("f")]
    public string Feminine { get; set; } = string.Empty;

    [JsonPropertyName("m")]
    public string Masculine { get; set; } = string.Empty;
}