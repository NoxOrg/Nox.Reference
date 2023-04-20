using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Country.DataContext;

public class RestcountryMaps : IMaps
{
    [JsonPropertyName("googleMaps")]
    public string GoogleMaps { get; set; } = string.Empty;

    [JsonPropertyName("openStreetMaps")]
    public string OpenStreetMaps { get; set; } = string.Empty;
}