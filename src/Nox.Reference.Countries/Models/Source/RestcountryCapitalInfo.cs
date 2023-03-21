
using System.Text.Json.Serialization;

namespace Nox.Reference.Countries;

public class RestcountryCapitalInfo : ICapitalInfo
{
    [JsonPropertyName("latlng")]
    public IReadOnlyList<decimal> LatLong { get; set; } = null!;
}
