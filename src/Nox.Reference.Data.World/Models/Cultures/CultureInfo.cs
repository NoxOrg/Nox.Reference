using Nox.Reference.Abstractions.Cultures;
using Nox.Reference.Common;
using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models.Cultures
{
    internal class CultureInfo : ICultureInfo
    {
        [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
        [JsonPropertyName("formalName")] public string FormalName { get; set; } = string.Empty;
        [JsonPropertyName("nativeName")] public string NativeName { get; set; } = string.Empty;
        [JsonPropertyName("commonName")] public string? CommonName { get; set; }
        [JsonPropertyName("language")] public string Language {  get; set; } = string.Empty;
        [JsonPropertyName("country")] public string Country { get; set; } = string.Empty;
        [JsonPropertyName("displayName")] public string DisplayName { get; set; } = string.Empty;
        [JsonPropertyName("displayNameWithDialect")] public string DisplayNameWithDialect { get; set; } = string.Empty;
        [JsonPropertyName("characterOrientation")] public string CharacterOrientation { get; set; } = string.Empty;
        [JsonPropertyName("lineOrientation")] public string LineOrientation { get; set; } = string.Empty;
        [JsonPropertyName("languageIso_639_2t")] public string? LanguageIso_639_2t { get; set; }

        [JsonConverter(typeof(NoxRefenceInfoJsonConverter<INumberFormatInfo, NumberFormatInfo>))]
        [JsonPropertyName("numberFormat")] public INumberFormatInfo NumberFormat { get; set; } = new NumberFormatInfo();

        [JsonConverter(typeof(NoxRefenceInfoJsonConverter<IDateFormatInfo, DateFormatInfo>))]
        [JsonPropertyName("dateFormat")] public IDateFormatInfo DateFormat { get; set; } = new DateFormatInfo();
    }
}
