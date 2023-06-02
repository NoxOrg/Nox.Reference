using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class CountryCapital : NoxReferenceEntityBase
{
    public string Name { get; internal set; } = string.Empty;
    public virtual GeoCoordinates? GeoCoordinates { get; internal set; }
}