using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

internal class CapitalInfo
{
    [JsonPropertyName("latlng")]
    public IReadOnlyList<decimal> LatLong { get; set; } = new List<decimal>();

    [JsonPropertyName("getCoordinates")]
    public GeoCoordinatesInfo GeoCoordinates { get; set; } = new GeoCoordinatesInfo();
}