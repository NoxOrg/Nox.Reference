using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

public class LocalHolidayNameInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;
}