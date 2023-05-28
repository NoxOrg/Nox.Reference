using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World;

public class GeoCoordinatesInfo
{
    [JsonPropertyName("latitude")]
    public decimal? Latitude { get; set; } = null;

    [JsonPropertyName("longitude")]
    public decimal? Longitude { get; set; } = null;
}