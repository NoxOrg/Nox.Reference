using System.Text.Json.Serialization;
using Nox.Reference.Abstractions.Shared;

namespace Nox.Reference.Data.World;

public class GeoCoordinatesInfo : IGeoCoordinates
{
    [JsonPropertyName("latitude")]
    public decimal? Latitude { get; set; } = null;

    [JsonPropertyName("longitude")]
    public decimal? Longitude { get; set; } = null;
}