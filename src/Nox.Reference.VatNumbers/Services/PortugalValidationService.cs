using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Services
{
    // Verifiend manually: FALSE
    // Personal data cleaned: TRUE
    internal class PortugalValidationService : VatValidationServiceBase
    {
        // TODO: review
        private const string _validationPattern = @"^\d{9}$";
        private const string _validationPatternDescription = "VAT should have 9 numeric characters";

        public override ValidationResult ValidateVatNumber(IVatNumber vatNumber)
        {
            // Cannot have special characters
            // Can have or not have 'PT' prefix
            var number = vatNumber.NormalizeVatNumber();

            if (string.IsNullOrWhiteSpace(number))
            {
                return ValidationResults.NullValidationResult;
            }

            var result = new ValidationResult();

            // Code should match the pattern
            result.ValidationErrors.AddRange(ValidateRegex(number, _validationPattern, vatNumber.Number, _validationPatternDescription));

            // Should be consisting of numbers to check checksum
            result.ValidationErrors.AddRange(number.ValidateCustomChecksum(CalculateChecksum));

            return result;
        }

        private static List<string> CalculateChecksum(string number)
        {
            var errorMessage = new List<string>();

            var minimumLengthRequirement = 9;
            if (number.Length < minimumLengthRequirement)
            {
                errorMessage.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLengthRequirement));
                return errorMessage;
            }
            int[] multipliers = { 9, 8, 7, 6, 5, 4, 3, 2 };

            var sum = 0;

            for (var index = 0; index < multipliers.Length; index++)
            {
                var digit = multipliers[index];
                sum += int.Parse(number[index].ToString()) * digit;
            }

            var checkDigit = 11 - sum % 11;
            if (checkDigit > 9)
            {
                checkDigit = 0;
            }

            bool isValid = checkDigit == int.Parse(number[8].ToString());
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }
    }
}
