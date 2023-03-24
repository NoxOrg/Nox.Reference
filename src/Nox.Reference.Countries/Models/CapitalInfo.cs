using System.Text.Json.Serialization;

namespace Nox.Reference.Countries;

public class CapitalInfo : ICapitalInfo
{
    [JsonPropertyName("geoCoordinates")] public GeoCoordinates GeoCoordinates_ { get; set; } = new GeoCoordinates();
    [JsonIgnore] public IGeoCoordinates GeoCoordinates => GeoCoordinates_;
}
