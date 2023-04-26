using Nox.Reference.Abstractions.Cultures;
using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models.Cultures
{
    internal class CultureInfo : ICultureInfo
    {
        [JsonPropertyName("id")] public string Id_ { get; set; } = string.Empty;
        [JsonPropertyName("formalName")] public string FormalName_ { get; set; } = string.Empty;
        [JsonPropertyName("nativeName")] public string NativeName_ { get; set; } = string.Empty;
        [JsonPropertyName("commonName")] public string? CommonName_ { get; set; }
        [JsonPropertyName("language")] public string Language_ {  get; set; } = string.Empty;
        [JsonPropertyName("country")] public string Country_ { get; set; } = string.Empty;
        [JsonPropertyName("displayName")] public string DisplayName_ { get; set; } = string.Empty;
        [JsonPropertyName("displayNameWithDialect")] public string DisplayNameWithDialect_ { get; set; } = string.Empty;
        [JsonPropertyName("characterOrientation")] public string CharacterOrientation_ { get; set; } = string.Empty;
        [JsonPropertyName("lineOrientation")] public string LineOrientation_ { get; set; } = string.Empty;
        [JsonPropertyName("languageIso_639_2t")] public string? LanguageIso_639_2t_ { get; set; }

        [JsonPropertyName("numberFormat")] public NumberFormatInfo NumberFormat_ { get; set; } = new NumberFormatInfo();
        [JsonPropertyName("dateFormat")] public DateFormatInfo DateFormat_ { get; set; } = new DateFormatInfo();

        [JsonIgnore] public string Id => Id_;
        [JsonIgnore] public string FormalName => FormalName_;
        [JsonIgnore] public string NativeName => NativeName_;
        [JsonIgnore] public string? CommonName => CommonName_;
        [JsonIgnore] public string Language => Language_;
        [JsonIgnore] public string Country => Country_;
        [JsonIgnore] public string DisplayName => DisplayName_;
        [JsonIgnore] public string DisplayNameWithDialect => DisplayNameWithDialect_;
        [JsonIgnore] public string CharacterOrientation => CharacterOrientation_;
        [JsonIgnore] public string LineOrientation => LineOrientation_;
        [JsonIgnore] public string? LanguageIso_639_2t => LanguageIso_639_2t_;

        [JsonIgnore] public INumberFormatInfo NumberFormat => NumberFormat_;
        [JsonIgnore] public IDateFormatInfo DateFormat => DateFormat_;
    }
}
