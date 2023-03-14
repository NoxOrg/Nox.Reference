
using System.Text.Json.Serialization;

namespace Nox.Reference.Countries;

public class RestcountryVehicleInfo : IVehicleInfo
{
    [JsonPropertyName("signs")]
    public string[] InternationalRegistrationCodes { get; set; } = Array.Empty<string>();

    [JsonPropertyName("side")]
    public string DrivingSide { get; set; } = string.Empty;
}
