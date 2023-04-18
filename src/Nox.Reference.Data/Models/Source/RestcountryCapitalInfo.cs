using System.Text.Json.Serialization;
using Nox.Reference.Abstractions.Countries;

namespace Nox.Reference.Country.DataContext;

/// <summary>
/// This class is used as a rest model and serialization model
/// for capital info from country data source
/// </summary>
public class RestcountryCapitalInfo : ICapitalInfo
{
    [JsonPropertyName("latlng")]
    public IReadOnlyList<decimal> LatLong { get; set; } = null!;

    [JsonPropertyName("getCoordinates")]
    public IGeoCoordinates GeoCoordinates { get; set; }
}