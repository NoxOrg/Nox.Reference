using Nox.Reference.Abstractions.Shared;

namespace Nox.Reference.Data.World;

internal class GeoCoordinates : IGeoCoordinates
{
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
}