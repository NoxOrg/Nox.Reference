using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

public class MapsInfo
{
    [JsonPropertyName("googleMaps")]
    public string GoogleMaps { get; set; } = string.Empty;

    [JsonPropertyName("openStreetMaps")]
    public string OpenStreetMaps { get; set; } = string.Empty;
}