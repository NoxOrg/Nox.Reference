using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

public class CountryNamesInfo
{
    [JsonPropertyName("common")]
    public string CommonName { get; set; } = string.Empty;

    [JsonPropertyName("official")]
    public string OfficialName { get; set; } = string.Empty;

    [JsonPropertyName("nativeName")]
    public Dictionary<string, CountryNameTranslationInfo>? NativeNamesDictionary { get; set; } = null!;

    [JsonIgnore]
    public IReadOnlyList<CountryNameTranslationInfo>? NativeNames => NativeNamesDictionary?
        .Select(kv => new CountryNameTranslationInfo
        {
            Language = kv.Key,
            CommonName = kv.Value.CommonName,
            OfficialName = kv.Value.OfficialName,
        }).ToList();
}