using System.Text.Json.Serialization;

namespace Nox.Reference;

public class TaxNumberDefinitionInfo
{
    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;

    [JsonPropertyName("localName")]
    public string LocalName { get; set; } = string.Empty;

    [JsonPropertyName("validations")]
    public ValidationInfo[]? Validations { get; set; } = Array.Empty<ValidationInfo>();

    [JsonPropertyName("verificationApi")]
    public VerificationApi VerificationApi { get; set; } = VerificationApi.None;
}