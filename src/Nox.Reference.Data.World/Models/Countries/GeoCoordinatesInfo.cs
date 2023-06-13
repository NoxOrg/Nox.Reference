using System.Text.Json.Serialization;

namespace Nox.Reference;

public class GeoCoordinatesInfo
{
    [JsonPropertyName("latitude")]
    public decimal? Latitude { get; set; } = null;

    [JsonPropertyName("longitude")]
    public decimal? Longitude { get; set; } = null;
}