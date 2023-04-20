using Nox.Reference.Abstractions.Languages;
using System.Text.Json.Serialization;

namespace Nox.Reference.Languages.Models
{
    public class LanguageInfo : ILanguageInfo
    {
        [JsonPropertyName("name")] public string Name_ { get; set; } = string.Empty;
        [JsonPropertyName("iso_639_1")] public string? Iso_639_1_ { get; set; }
        [JsonPropertyName("iso_639_2b")] public string? Iso_639_2b_ { get; set; }
        [JsonPropertyName("iso_639_2t")] public string? Iso_639_2t_ { get; set; }
        [JsonPropertyName("iso_639_3")] public string Iso_639_3_ { get; set; } = string.Empty;
        [JsonPropertyName("common")] public bool Common_ { get; set; }
        [JsonPropertyName("type")] public LanguageType Type_ { get; set; }
        [JsonPropertyName("scope")] public LanguageScope Scope_  { get; set; }
        [JsonPropertyName("wikiUrl")] public string? WikiUrl_ { get; set; }
        [JsonPropertyName("nameTranslations")] public List<LanguageTranslation> NameTranslations_ { get; set; } = new List<LanguageTranslation>();

        [JsonIgnore] public string Name => Name_;
        [JsonIgnore] public string? Iso_639_1 => Iso_639_1_;
        [JsonIgnore] public string Iso_639_3 => Iso_639_3_;
        [JsonIgnore] public string? Iso_639_2b => Iso_639_2b_;
        [JsonIgnore] public string? Iso_639_2t => Iso_639_2t_;
        [JsonIgnore] public bool Common => Common_;
        [JsonIgnore] public LanguageType Type => Type_;
        [JsonIgnore] public LanguageScope Scope => Scope_;
        [JsonIgnore] public string? WikiUrl => WikiUrl_;
        [JsonIgnore] public IList<ILanguageTranslation> NameTranslations => NameTranslations_
            .Select(x => (ILanguageTranslation)x)
            .ToList();
    }
}