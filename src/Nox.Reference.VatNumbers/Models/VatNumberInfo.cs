using Nox.Reference.Abstractions.VatNumbers;
using Nox.Reference.Shared;
using System.Text.Json.Serialization;

namespace Nox.Reference.VatNumbers.Models
{
    public class VatNumberInfo : IVatNumberInfo
    {
        public VatNumberInfo() { }

        public VatNumberInfo(string countryIso2Code)
        {
            CountryIso2Code = countryIso2Code;
            OriginalVatNumber = string.Empty;
        }

        public VatNumberInfo(string countryIso2Code, string vatNumber)
        {
            CountryIso2Code = countryIso2Code;
            OriginalVatNumber = vatNumber;
        }

        public VatNumberInfo(IVatNumberInfo vatNumberInfo)
        {
            CountryIso2Code = vatNumberInfo?.CountryIso2Code ?? string.Empty;
            OriginalVatNumber = vatNumberInfo?.OriginalVatNumber ?? string.Empty;
            ValidationRegex = vatNumberInfo?.ValidationRegex ?? string.Empty;
            InputMask = vatNumberInfo?.InputMask ?? string.Empty;
            FormattedVatNumber = vatNumberInfo?.FormattedVatNumber ?? string.Empty;
            ValidationResult = vatNumberInfo?.ValidationResult ?? new ValidationResult();
            IsVerified = vatNumberInfo?.IsVerified ?? false;
            ValidationFormatDescription = vatNumberInfo?.ValidationFormatDescription ?? string.Empty;
            //IsUsingCustomValidation = vatNumberInfo?.IsUsingCustomValidation ?? false;
            //IsUsingNullCheck = vatNumberInfo?.IsUsingNullCheck ?? false;
            //IsUsingRegexValidation = vatNumberInfo?.IsUsingRegexValidation ?? false;
            //IsUsingChecksumValidation = vatNumberInfo?.IsUsingChecksumValidation ?? false;
            //ChecksumAlgorithm = vatNumberInfo?.ChecksumAlgorithm ?? string.Empty;
        }

        // Taken from constructor
        [JsonPropertyName("alphaCode2")] public string CountryIso2Code { get; set; } = string.Empty;
        public string OriginalVatNumber { get; set; } = string.Empty;

        // Enriched from database

        [JsonPropertyName("validationRegex")] public string ValidationRegex { get; set; } = string.Empty;
        [JsonPropertyName("validationFormatDescription")] public string ValidationFormatDescription { get; set; } = string.Empty;
        [JsonPropertyName("inputMask")] public string InputMask { get; set; } = string.Empty;

        // Optionally set on runtime
        public string FormattedVatNumber { get; set; } = string.Empty;
        public IValidationResult ValidationResult { get; set; } = new ValidationResult();
        public bool IsVerified { get; set; } = false;
        public IValidationInfo ValidationInfo { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}