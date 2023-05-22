using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

internal class LanguageInfoAdditionalInfo
{
    [JsonPropertyName("639-1")] public string? Iso_639_1 { get; set; }
    [JsonPropertyName("639-2")] public string Iso_639_2 { get; set; } = string.Empty;
    [JsonPropertyName("wikiUrl")] public string? WikiUrl { get; set; }
    [JsonPropertyName("de")] public List<string>? GermanName { get; set; }
    [JsonPropertyName("fr")] public List<string>? FrenchName { get; set; }
}