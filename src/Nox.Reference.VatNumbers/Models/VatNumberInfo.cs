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
        [JsonPropertyName("validations")] public List<ValidationInfo>? Validations_ { get; set; }

        // TODO: improve this
        [JsonIgnore] public List<IValidationInfo>? Validations
        { 
            get
            { 
                return Validations_?
                    .Select(x => (IValidationInfo)x)
                    .ToList();
            }
            set
            {
                if (value == null)
                {
                    Validations_ = null;
                    return;
                }

                Validations_ = value!
                    .OfType<ValidationInfo>()
                    .Select(x => x)
                    .ToList();
            }
        }
        [JsonPropertyName("verificationApi")] public string VerificationApi { get; set; } = string.Empty;

        // Optionally set on runtime
        public string FormattedVatNumber { get; set; } = string.Empty;
        public ValidationResult ValidationResult_ { get; set; } = new ValidationResult();
        public IValidationResult ValidationResult
        {
            get => ValidationResult_;
            set
            {
                // TODO: should we replace base class IValidationResult with ValidationResult?
                if (value is ValidationResult result)
                {
                    ValidationResult_ = result;
                    return;
                }

                throw new NotSupportedException("Only ValidationResult type is supported, please implement a custom VatNumber info class for a different implementation.");
            }
        }
        public bool IsVerified { get; set; } = false;
    }
}