using Nox.Reference.Abstractions;
using Nox.Reference.Common;
using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World;

public class VatNumberDefinitionInfo : IVatNumberDefinitionInfo
{
    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;

    [JsonPropertyName("localName")]
    public string LocalName { get; set; } = string.Empty;

    [JsonPropertyName("validations")]
    [JsonConverter(typeof(NoxRefenceInfoJsonConverter<IValidationInfo[], ValidationInfo[]>))]
    public IValidationInfo[]? Validations { get; set; } = Array.Empty<ValidationInfo>();

    [JsonPropertyName("verificationApi")]
    public VerificationApi VerificationApi { get; set; } = VerificationApi.None;
}