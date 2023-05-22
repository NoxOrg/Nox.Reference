using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World;

internal class GeoCoordinatesInfo
{
    [JsonPropertyName("latitude")]
    public decimal? Latitude { get; set; } = null;

    [JsonPropertyName("longitude")]
    public decimal? Longitude { get; set; } = null;
}