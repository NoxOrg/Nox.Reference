
using System.Text.Json.Serialization;

namespace Nox.Reference.Countries;

public class CountryNames : ICountryNames
{
    public string CommonName { get; set; } = string.Empty;
    public string OfficialName { get; set; } = string.Empty;
    [JsonPropertyName("nativeNames")]public List<NativeNameInfo>? NativeNames_ { get; }
    [JsonIgnore]public IReadOnlyList<INativeNameInfo>? NativeNames => NativeNames_;
}
