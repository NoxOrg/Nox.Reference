using System.Text.Json.Serialization;

namespace Nox.Reference;

internal class RestIsoData
{
    [JsonPropertyName("code")] public string Code { get; set; } = "";
    [JsonPropertyName("number")] public string Number { get; set; } = "";
}