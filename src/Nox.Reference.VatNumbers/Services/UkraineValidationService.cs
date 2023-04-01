using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Services
{
    // Verifiend manually: FALSE
    // Personal data cleaned: TRUE
    internal class UkraineValidationService : VatValidationServiceBase
    {
        private const string _validationPattern = @"^\d{8,12}$";
        private const string _validationPatternDescription = "VAT should have from 8 to 10 numeric characters";

        public override ValidationResult ValidateVatNumber(IVatNumber vatNumber)
        {
            // Cannot have special characters
            // Can have or not have 'ua' prefix
            var number = vatNumber.NormalizeVatNumber();

            if (string.IsNullOrWhiteSpace(number))
            {
                return ValidationResults.NullValidationResult;
            }

            var result = new ValidationResult();

            // Code should match the pattern
            result.ValidationErrors.AddRange(ValidateRegex(number, _validationPattern, vatNumber.Number, _validationPatternDescription));

            return result;
        }
    }
}
