using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Services
{
    // Verifiend manually: FALSE
    // Personal data cleaned: TRUE
    internal class PolandValidationService : VatValidationServiceBase
    {
        // TODO: review
        private const string _validationPattern = @"^\d{10}$";
        private const string _validationPatternDescription = "VAT should have 10 numeric characters";

        public override ValidationResult ValidateVatNumber(IVatNumber vatNumber)
        {
            // Cannot have special characters
            // Can have or not have 'PL' prefix
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

            var exactLengthRequirement = 10;
            if (number.Length < exactLengthRequirement)
            {
                errorMessage.Add(string.Format(ValidationErrors.LengthShouldEqualError, exactLengthRequirement));
                return errorMessage;
            }

            int[] multipliers = { 6, 5, 7, 2, 3, 4, 5, 6, 7 };

            var sum = 0;

            for (var index = 0; index < multipliers.Length; index++)
            {
                var digit = multipliers[index];
                sum += int.Parse(number[index].ToString()) * digit;
            }

            var checkDigit = sum % 11;

            if (checkDigit > 9)
            {
                checkDigit = 0;
            }

            bool isValid = checkDigit == int.Parse(number[9].ToString());
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }
    }
}
