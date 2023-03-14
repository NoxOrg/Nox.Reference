
using System.Text.Json.Serialization;

namespace Nox.Reference.Countries;

public class RestcountryMaps : IMaps
{
    [JsonPropertyName("googleMaps")]
    public string GoogleMaps { get; set; } = string.Empty;

    [JsonPropertyName("openStreetMaps")]
    public string OpenStreetMaps { get; set; } = string.Empty;
}
