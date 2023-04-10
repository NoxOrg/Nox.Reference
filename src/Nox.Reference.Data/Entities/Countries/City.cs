namespace Nox.Reference.Data;

internal class City : INoxReferenceEntity
{
    public int Id { get; set; }
    public GeoCoordinates GeoCoordinates { get; set; }
    public IReadOnlyList<PostalCode> PostalCodes { get; set; }
}