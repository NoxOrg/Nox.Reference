using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

public class CountryNamesInfo
{
    [JsonPropertyName("common")]
    public string CommonName { get; set; } = string.Empty;

    [JsonPropertyName("official")]
    public string OfficialName { get; set; } = string.Empty;

    [JsonPropertyName("nativeName")]
    public Dictionary<string, NativeNameInfo>? NativeNames1 { get; set; } = null!;

    [JsonIgnore]
    public IReadOnlyList<NativeNameInfo>? NativeNames => NativeNames1?
        .Select(kv => new NativeNameInfo
        {
            Language = kv.Key,
            CommonName = kv.Value.CommonName,
            OfficialName = kv.Value.OfficialName,
        }).ToList();
}