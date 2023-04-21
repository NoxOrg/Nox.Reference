using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

public class CapitalInfo : ICapitalInfo
{
    [JsonPropertyName("geoCoordinates")] public GeoCoordinatesInfo GeoCoordinates_ { get; set; } = new GeoCoordinatesInfo();
    [JsonIgnore] public IGeoCoordinates GeoCoordinates => GeoCoordinates_;
}