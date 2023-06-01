using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class GeoCoordinates : WorldNoxReferenceEntity
{
    public decimal? Latitude { get; internal set; }
    public decimal? Longitude { get; internal set; }
}