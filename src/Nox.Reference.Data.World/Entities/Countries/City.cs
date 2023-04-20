using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext;

internal class City : INoxReferenceEntity
{
    public int Id { get; set; }
    public GeoCoordinates GeoCoordinates { get; set; }
    public IReadOnlyList<PostalCode> PostalCodes { get; set; }
}