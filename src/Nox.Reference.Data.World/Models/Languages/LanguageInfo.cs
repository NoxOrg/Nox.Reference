using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

internal class LanguageInfo
{
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    [JsonPropertyName("iso_639_1")] public string? Iso_639_1 { get; set; }
    [JsonPropertyName("iso_639_2b")] public string? Iso_639_2b { get; set; }
    [JsonPropertyName("iso_639_2t")] public string? Iso_639_2t { get; set; }
    [JsonPropertyName("iso_639_3")] public string Iso_639_3 { get; set; } = string.Empty;
    [JsonPropertyName("common")] public bool Common { get; set; }
    [JsonPropertyName("type")] public LanguageType Type { get; set; }
    [JsonPropertyName("scope")] public LanguageScope Scope { get; set; }
    [JsonPropertyName("wikiUrl")] public string? WikiUrl { get; set; }

    [JsonPropertyName("nameTranslations")]
    public IList<LanguageTranslationInfo> NameTranslations { get; set; } = new List<LanguageTranslationInfo>();
}