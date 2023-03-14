
using System.Text.Json.Serialization;

namespace Nox.Reference.Countries;

public class RestcountryCapitalInfo : ICapitalInfo
{
    [JsonPropertyName("latlng")]
    public decimal[] LatLong { get; set; } = null!;
}
