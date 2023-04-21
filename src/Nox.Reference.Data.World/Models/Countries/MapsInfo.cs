using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

public class MapsInfo : IMaps
{
    public string GoogleMaps { get; set; } = string.Empty;

    public string OpenStreetMaps { get; set; } = string.Empty;
}