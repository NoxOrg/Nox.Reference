namespace Nox.Reference;

public class CountryCapital : NoxReferenceEntityBase
{
    public string Name { get; internal set; } = string.Empty;
    public virtual GeoCoordinates? GeoCoordinates { get; internal set; }
}