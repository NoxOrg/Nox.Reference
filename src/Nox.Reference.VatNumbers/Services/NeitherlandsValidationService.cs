using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Services
{
    // Verifiend manually: FALSE
    // Personal data cleaned: TRUE
    internal class NeitherlandsValidationService : IVatValidationService
    {
        // TODO: review
        private const string _validationPattern = @"^\d{9}b\d{2}$";
        private const string _validationPatternDescription = "VAT should have 9 numeric characters first, then B letter and 2 numbers after it.";

        public ValidationResult ValidateVatNumber(IVatNumber vatNumber)
        {
            // Cannot have special characters
            // Can have or not have 'NL' prefix
            var number = vatNumber.NormalizeVatNumber();

            if (string.IsNullOrWhiteSpace(number))
            {
                return ValidationResults.NullValidationResult;
            }

            var result = new ValidationResult();

            // Code should match the pattern
            IVatValidationService.ValidateRegex(result, number, _validationPattern, vatNumber.Number, _validationPatternDescription);

            var minimumLengthRequirement = 9;
            if (number.Length < minimumLengthRequirement)
            {
                result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLengthRequirement));
            }
            else
            {
                // Should be consisting of numbers to check checksum
                number.Substring(0, 9).ValidateCustomChecksum(result, CalculateChecksum);
            }

            return result;
        }

        private static bool CalculateChecksum(string number, ValidationResult result)
        {
            int[] multipliers = { 9, 8, 7, 6, 5, 4, 3, 2 };
            var sum = number.GetSumOfDigitsMulipliedByMultipliers(multipliers);

            var checkDigit = sum % 11;

            if (checkDigit > 9)
            {
                checkDigit = 0;
            }

            return checkDigit == int.Parse(number[8].ToString());
        }
    }
}
