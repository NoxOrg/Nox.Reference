using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

public class NativeNameInfo
{
    [JsonIgnore]
    public string Language { get; set; } = string.Empty;

    [JsonPropertyName("official")]
    public string OfficialName { get; set; } = string.Empty;

    [JsonPropertyName("common")]
    public string CommonName { get; set; } = string.Empty;
}