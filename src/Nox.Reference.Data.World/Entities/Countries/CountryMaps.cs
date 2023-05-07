using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class CountryMaps : INoxReferenceEntity
{
    public int Id { get; private set; }
    public string GoogleMaps { get; set; } = string.Empty;

    public string OpenStreetMaps { get; set; } = string.Empty;
}