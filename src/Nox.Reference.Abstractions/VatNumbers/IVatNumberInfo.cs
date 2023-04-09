using Nox.Reference.Shared;

namespace Nox.Reference.Abstractions.VatNumbers
{
    public interface IVatNumberInfo
    {
        public string Country { get; set; }
        public string LocalName { get; set; }
        public string VerificationApi { get; set; }
        public string OriginalVatNumber { get; set; }
        public string FormattedVatNumber { get; set; }

        public bool IsVerified { get; set; }
        public IValidationResult ValidationResult { get; set; }

        public List<IValidationInfo>? Validations { get; set; }
    }
}
