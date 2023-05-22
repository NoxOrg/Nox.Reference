using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World.Models;

public class VehicleInfo
{
    [JsonPropertyName("signs")]
    public IReadOnlyList<string> InternationalRegistrationCodes { get; set; } = new List<string>();

    [JsonPropertyName("side")]
    public string DrivingSide { get; set; } = string.Empty;
}