using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

public class CapitalInfo : ICapitalInfo
{
    [JsonPropertyName("geoCoordinates")] public IGeoCoordinates GeoCoordinates { get; set; } = new GeoCoordinatesInfo();
}