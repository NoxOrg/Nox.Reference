using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Services
{
    // Verifiend manually: FALSE
    // Personal data cleaned: TRUE
    internal class SouthAfricaValidationService : VatValidationServiceBase
    {
        // TODO: review
        private const string _validationPattern = @"^[012349]\d{9}$";
        private const string _validationPatternDescription = "VAT should have 10 numeric characters and start with [0|1|2|3|4|9]";

        public override ValidationResult ValidateVatNumber(IVatNumber vatNumber)
        {
            // Cannot have special characters
            // Can have or not have 'ZA' prefix
            var number = vatNumber.NormalizeVatNumber();

            if (string.IsNullOrWhiteSpace(number))
            {
                return ValidationResults.NullValidationResult;
            }

            var result = new ValidationResult();

            // Code should match the pattern
            result.ValidationErrors.AddRange(ValidateRegex(number, _validationPattern, vatNumber.Number, _validationPatternDescription));

            // Should be consisting of numbers to check checksum
            result.ValidationErrors.AddRange(number.ValidateLuhnDigitForVatNumber());

            return result;
        }
    }
}
