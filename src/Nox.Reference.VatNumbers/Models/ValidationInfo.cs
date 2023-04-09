using Nox.Reference.Abstractions.VatNumbers;
using System.Text.Json.Serialization;

namespace Nox.Reference.VatNumbers.Models
{
    public class ValidationInfo : IValidationInfo
    {
        [JsonPropertyName("regex")] public string Regex { get; set; } = string.Empty;
        [JsonPropertyName("validationFormatDescription")] public string ValidationFormatDescription { get; set; } = string.Empty;
        [JsonPropertyName("inputMask")] public string InputMask { get; set; } = string.Empty;
        [JsonPropertyName("minimumLength")] public int MinimumLength { get; set; }
        [JsonPropertyName("maximumLength")] public int MaximumLength { get; set; }
        [JsonPropertyName("checksum")] public ChecksumInfo? Checksum_ { get; set; }
        [JsonIgnore] public IChecksumInfo? Checksum => Checksum_;
    }
}
