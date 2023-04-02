using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Services
{
    // Verifiend manually: FALSE
    // Personal data cleaned: TRUE
    internal class IndiaValidationService : VatValidationServiceBase
    {
        // TODO: review
        private const string _validationPattern = @"^\d{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[0-9A-Z]{3}$";
        private const string _validationPatternDescription = "VAT should have 2 numeric characters, then 5 charaters, then 4 numbers, then 1 character, then 3 numbers or characters.";

        public override ValidationResult ValidateVatNumber(IVatNumber vatNumber)
        {
            // Cannot have special characters
            // Can have or not have 'IN' prefix
            var number = vatNumber.NormalizeVatNumber();

            if (string.IsNullOrWhiteSpace(number))
            {
                return ValidationResults.NullValidationResult;
            }

            var result = new ValidationResult();

            // Code should match the pattern
            result.ValidationErrors.AddRange(ValidateRegex(number, _validationPattern, vatNumber.Number, _validationPatternDescription));

            // TODO: it's possible to calculate checksum

            return result;
        }
    }
}
