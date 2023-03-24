using System.Text.Json.Serialization;

namespace Nox.Reference.Currencies.Models.Rest
{
    public class RestIsoData
    {
        [JsonPropertyName("code")] public string Code { get; set; } = "";
        [JsonPropertyName("number")] public string Number { get; set; } = "";
    }
}
