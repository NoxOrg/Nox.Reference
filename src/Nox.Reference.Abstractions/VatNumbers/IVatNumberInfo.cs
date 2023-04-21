using Nox.Reference.Shared;

namespace Nox.Reference.Abstractions
{
    public interface IVatNumberInfo
    {
        public string Country { get; set; }
        public string LocalName { get; set; }
        public VerificationApi VerificationApi { get; set; }
        public string OriginalVatNumber { get; set; }
        public string FormattedVatNumber { get; set; }

        public bool IsVerified { get; set; }
        public IValidationResult ValidationResult { get; set; }

        public IValidationInfo[]? Validations { get; set; }
        public object? ApiVerificationData { get; set; }
    }
}
