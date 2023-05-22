using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class GeoCoordinates : INoxReferenceEntity
{
    public int Id { get; private set; }
    public decimal? Latitude { get; internal set; }
    public decimal? Longitude { get; internal set; }
}