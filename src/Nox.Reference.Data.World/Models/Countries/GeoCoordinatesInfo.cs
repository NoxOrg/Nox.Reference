using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

internal class GeoCoordinatesInfo : IGeoCoordinates
{
    [JsonPropertyName("latitude")]
    public decimal? Latitude { get; set; } = null;

    [JsonPropertyName("longitude")]
    public decimal? Longitude { get; set; } = null;
}