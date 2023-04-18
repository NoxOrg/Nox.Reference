using Nox.Reference.Abstractions.Languages;
using System.Text.Json.Serialization;

namespace Nox.Reference.Languages.Models
{
    public class LanguageInfo : ILanguageInfo
    {
        [JsonPropertyName("english_name")] public string EnglishName_ { get; set; } = string.Empty;
        [JsonPropertyName("iso_639_1")] public string? Iso_639_1_ { get; set; }
        [JsonPropertyName("iso_639_3")] public string Iso_639_3_ { get; set; } = string.Empty;
        [JsonPropertyName("iso_639_2b")] public string? Iso_639_2b_ { get; set; }
        [JsonPropertyName("iso_639_2t")] public string? Iso_639_2t_ { get; set; }
        [JsonPropertyName("common")] public bool Common_ { get; set; }
        [JsonPropertyName("type")] public LanguageType Type_ { get; set; }
        [JsonPropertyName("scope")] public LanguageScope Scope_  { get; set; }
        [JsonPropertyName("german_name")] public string? GermanName_ { get; set; }
        [JsonPropertyName("french_name")] public string? FrenchName_ { get; set; }
        [JsonPropertyName("wiki_url")] public string? WikiUrl_ { get; set; }
        [JsonPropertyName("native_name")] public string? NativeName_ { get; set; }

        [JsonIgnore] public string EnglishName => EnglishName_;
        [JsonIgnore] public string? Iso_639_1 => Iso_639_1_;
        [JsonIgnore] public string Iso_639_3 => Iso_639_3_;
        [JsonIgnore] public string? Iso_639_2b => Iso_639_2b_;
        [JsonIgnore] public string? Iso_639_2t => Iso_639_2t_;
        [JsonIgnore] public bool Common => Common_;
        [JsonIgnore] public LanguageType Type => Type_;
        [JsonIgnore] public LanguageScope Scope => Scope_;
        [JsonIgnore] public string? GermanName => GermanName_;
        [JsonIgnore] public string? FrenchName => FrenchName_;
        [JsonIgnore] public string? WikiUrl => WikiUrl_;
        [JsonIgnore] public string? NativeName => NativeName_;
    }
}