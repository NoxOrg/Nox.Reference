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
            Country = countryIso2Code;
            OriginalVatNumber = string.Empty;
        }

        public VatNumberInfo(string countryIso2Code, string vatNumber)
        {
            Country = countryIso2Code;
            OriginalVatNumber = vatNumber;
        }

        public VatNumberInfo(IVatNumberInfo vatNumberInfo)
        {
            Country = vatNumberInfo?.Country ?? string.Empty;
            OriginalVatNumber = vatNumberInfo?.OriginalVatNumber ?? string.Empty;
            LocalName = vatNumberInfo?.LocalName ?? string.Empty;
            Validations = vatNumberInfo?.Validations;
            FormattedVatNumber = vatNumberInfo?.FormattedVatNumber ?? string.Empty;
            ValidationResult = vatNumberInfo?.ValidationResult ?? new ValidationResult();
            IsVerified = vatNumberInfo?.IsVerified ?? false;
            VerificationApi = vatNumberInfo?.VerificationApi ?? string.Empty;
        }

        // Taken from constructor
        [JsonPropertyName("country")] public string Country { get; set; } = string.Empty;
        public string OriginalVatNumber { get; set; } = string.Empty;

        // Enriched from database

        [JsonPropertyName("localName")] public string LocalName { get; set; } = string.Empty;
        [JsonPropertyName("validations")] public ValidationInfo[]? Validations_ { get; set; }
        [JsonIgnore] public IValidationInfo[]? ValidationsOverride_ { get; set; }
        [JsonIgnore] public IValidationInfo[]? Validations
        {
            get
            {
                if (ValidationsOverride_ != null)
                {
                    return ValidationsOverride_;
                }

                return Validations_;
            }
            set
            {
                ValidationsOverride_ = value;
            }
        }
        [JsonPropertyName("verificationApi")] public string VerificationApi { get; set; } = string.Empty;

        // Optionally set on runtime
        public string FormattedVatNumber { get; set; } = string.Empty;
        public IValidationResult ValidationResult { get; set; } = new ValidationResult();
        public bool IsVerified { get; set; } = false;
    }
}