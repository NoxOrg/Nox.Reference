using Nox.Reference.Shared;

namespace Nox.Reference.Abstractions.VatNumbers
{
    public interface IVatNumberInfo
    {
        public string CountryIso2Code { get; set; }
        public string OriginalVatNumber { get; set; }
        public string ValidationRegex { get; set; }
        public string ValidationFormatDescription { get; set; }
        public string InputMask { get; set; }
        public string FormattedVatNumber { get; set; }
        public bool IsVerified { get; set; }
        public IValidationResult ValidationResult { get; set; }
        public IValidationInfo ValidationInfo { get; set; }
    }
}
