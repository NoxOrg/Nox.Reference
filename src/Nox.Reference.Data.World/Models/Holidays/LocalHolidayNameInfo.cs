using System.Text.Json.Serialization;

namespace Nox.Reference;

public class LocalHolidayNameInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;
}