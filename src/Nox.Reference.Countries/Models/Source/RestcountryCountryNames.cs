
using System.Text.Json.Serialization;

namespace Nox.Reference.Countries;

public class RestcountryCountryNames : ICountryNames
{
    [JsonPropertyName("common")]
    public string CommonName { get; set; } = string.Empty;

    [JsonPropertyName("official")]
    public string OfficialName { get; set; } = string.Empty;

    [JsonPropertyName("nativeName")]
    public Dictionary<string, RestcountryNativeNameInfo>? NativeNames1 { get; set; } = null!;

    [JsonIgnore]
    public IReadOnlyList<INativeNameInfo>? NativeNames => NativeNames1?
        .Select( kv => new RestcountryNativeNameInfo
        {
            Language = kv.Key, CommonName = kv.Value.CommonName, OfficialName = kv.Value.OfficialName,
        }).ToList();
}
