using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

/// <summary>
/// This class is used as a rest model and serialization model
/// for capital info from country data source
/// </summary>
public class RestcountryCapitalInfo : ICapitalInfo
{
    [JsonPropertyName("latlng")]
    public IReadOnlyList<decimal> LatLong { get; set; } = new List<decimal>();

    [JsonPropertyName("getCoordinates")]
    public IGeoCoordinates GeoCoordinates { get; set; } = new GeoCoordinatesInfo();
}