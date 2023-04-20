using Nox.Reference.Abstractions;
using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World;

public class LanguageTranslation : ILanguageTranslation
{
    [JsonPropertyName("translation")] public string Translation { get; set; } = string.Empty;
    [JsonPropertyName("language")] public string Language { get; set; } = string.Empty;
}