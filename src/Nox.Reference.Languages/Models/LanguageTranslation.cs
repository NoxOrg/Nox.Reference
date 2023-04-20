using Nox.Reference.Abstractions.Languages;
using System.Text.Json.Serialization;

namespace Nox.Reference.Languages.Models
{
    public class LanguageTranslation : ILanguageTranslation
    {
        [JsonPropertyName("translation")] public string Translation { get; set; } = string.Empty;
        [JsonPropertyName("language")] public string Language { get; set; } = string.Empty;
    }
}
