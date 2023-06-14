using System.Text.Json.Serialization;

namespace Nox.Reference;

public class CountryNameTranslationInfo
{
    [JsonIgnore]
    public string Language { get; set; } = string.Empty;

    [JsonPropertyName("official")]
    public string OfficialName { get; set; } = string.Empty;

    [JsonPropertyName("common")]
    public string CommonName { get; set; } = string.Empty;
}