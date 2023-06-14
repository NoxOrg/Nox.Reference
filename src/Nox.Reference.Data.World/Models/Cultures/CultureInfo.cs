using Nox.Reference.Common;
using System.Text.Json.Serialization;

namespace Nox.Reference;

public class CultureInfo
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
    [JsonPropertyName("formalName")] public string FormalName { get; set; } = string.Empty;
    [JsonPropertyName("nativeName")] public string NativeName { get; set; } = string.Empty;
    [JsonPropertyName("commonName")] public string? CommonName { get; set; }
    [JsonPropertyName("language")] public string Language { get; set; } = string.Empty;
    [JsonPropertyName("country")] public string? Country { get; set; }
    [JsonPropertyName("displayName")] public string DisplayName { get; set; } = string.Empty;
    [JsonPropertyName("displayNameWithDialect")] public string DisplayNameWithDialect { get; set; } = string.Empty;
    [JsonPropertyName("characterOrientation")] public string CharacterOrientation { get; set; } = string.Empty;
    [JsonPropertyName("lineOrientation")] public string LineOrientation { get; set; } = string.Empty;
    [JsonPropertyName("languageIso_639_2t")] public string? LanguageIso_639_2t { get; set; }

    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<NumberFormatInfo, NumberFormatInfo>))]
    [JsonPropertyName("numberFormat")] public NumberFormatInfo NumberFormat { get; set; } = new NumberFormatInfo();

    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<DateFormatInfo, DateFormatInfo>))]
    [JsonPropertyName("dateFormat")] public DateFormatInfo DateFormat { get; set; } = new DateFormatInfo();
}