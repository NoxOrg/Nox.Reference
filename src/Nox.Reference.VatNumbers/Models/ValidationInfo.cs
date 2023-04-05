using Nox.Reference.Abstractions.VatNumbers;
using System.Text.Json.Serialization;

namespace Nox.Reference.VatNumbers.Models
{
    public class ValidationInfo : IValidationInfo
    {
        [JsonPropertyName("isUsingManuallyImplementedValidation")] public bool IsUsingManuallyImplementedValidation { get; set; }
        [JsonPropertyName("isUsingNullCheck")] public bool IsUsingNullCheck { get; set; }
        [JsonPropertyName("isUsingRegexValidation")] public bool IsUsingRegexValidation { get; set; }
        [JsonPropertyName("isUsingChecksumValidation")] public bool IsUsingChecksumValidation { get; set; }
        [JsonPropertyName("checksumAlgorithm")] public string ChecksumAlgorithm { get; set; }
        [JsonPropertyName("minimumLengthCheck")] public int MinimumLengthCheck { get; set; }
        [JsonPropertyName("weights")] public List<int> Weights { get; set; }
        [JsonPropertyName("numberToDivideBy")] public int NumberToDivideBy { get; set; }
        [JsonPropertyName("checkDigitPosition")] public string СheckDigitPosition { get; set; }
    }
}
