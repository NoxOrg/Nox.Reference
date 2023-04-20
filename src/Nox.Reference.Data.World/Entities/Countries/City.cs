using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class City : INoxReferenceEntity
{
    public int Id { get; set; }
    public GeoCoordinates GeoCoordinates { get; set; }
    public IReadOnlyList<PostalCode> PostalCodes { get; set; }
}