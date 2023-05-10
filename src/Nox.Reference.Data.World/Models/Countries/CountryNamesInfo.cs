using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

public class CountryNamesInfo : ICountryNames
{
    public string CommonName { get; set; } = string.Empty;
    public string OfficialName { get; set; } = string.Empty;
    [JsonPropertyName("nativeNames")] public IReadOnlyList<INativeNameInfo>? NativeNames { get; set; }
}