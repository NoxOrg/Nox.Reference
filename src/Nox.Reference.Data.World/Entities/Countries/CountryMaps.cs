using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

public class CountryMaps : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string GoogleMaps { get; private set; } = string.Empty;
    public string OpenStreetMaps { get; private set; } = string.Empty;
}