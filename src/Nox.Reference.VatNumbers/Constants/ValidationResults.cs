using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Constants
{
    public static class ValidationResults
    {
        public static readonly ValidationResult CountryNotFoundValidationResult = new ValidationResult(ValidationErrors.ValidatorNotFoundError);
        public static readonly ValidationResult NullValidationResult = new ValidationResult(ValidationErrors.NullValueError);
    }
}
