using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class GeoCoordinates : INoxReferenceEntity
{
    public int Id { get; private set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
}