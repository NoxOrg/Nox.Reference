using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

public class RestcountryNativeNameInfo : INativeNameInfo
{
    [JsonIgnore]
    public string Language { get; set; } = string.Empty;

    [JsonPropertyName("official")]
    public string OfficialName { get; set; } = string.Empty;

    [JsonPropertyName("common")]
    public string CommonName { get; set; } = string.Empty;
}