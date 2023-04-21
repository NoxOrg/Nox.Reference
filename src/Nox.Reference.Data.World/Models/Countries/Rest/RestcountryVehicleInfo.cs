using System.Text.Json.Serialization;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

public class RestcountryVehicleInfo : IVehicleInfo
{
    [JsonPropertyName("signs")]
    public IReadOnlyList<string> InternationalRegistrationCodes { get; set; } = new List<string>();

    [JsonPropertyName("side")]
    public string DrivingSide { get; set; } = string.Empty;
}