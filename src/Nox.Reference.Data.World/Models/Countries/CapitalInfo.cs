using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;
using Nox.Reference.Abstractions.Shared;

namespace Nox.Reference.Data.World;

public class CapitalInfo : ICapitalInfo
{
    [JsonPropertyName("geoCoordinates")] public IGeoCoordinates GeoCoordinates { get; set; } = new GeoCoordinatesInfo();
}