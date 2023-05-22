using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

internal class LanguageTranslationInfo
{
    [JsonPropertyName("translation")] public string Translation { get; set; } = string.Empty;
    [JsonPropertyName("language")] public string Language { get; set; } = string.Empty;
}