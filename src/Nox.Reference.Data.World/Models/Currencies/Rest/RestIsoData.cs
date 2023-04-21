using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World;

public class RestIsoData
{
    [JsonPropertyName("code")] public string Code { get; set; } = "";
    [JsonPropertyName("number")] public string Number { get; set; } = "";
}