using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Services
{
    // Verifiend manually: FALSE
    // Personal data cleaned: TRUE
    internal class ColumbiaValidatorService : IVatValidationService
    {
        private const string _validationPattern = @"^[cj]{0,1}[0-9]{9,10}[cj]{0,1}$";
        private const string _validationPatternDescription = "VAT should have 9 or 10 numeric characters optionally with a letter C or J before or after the numeric code.";

        public ValidationResult ValidateVatNumber(IVatNumber vatNumber)
        {
            // Cannot have special characters
            // Can have or not have 'co' prefix
            var number = vatNumber.NormalizeVatNumber();

            if (string.IsNullOrWhiteSpace(number))
            {
                return ValidationResults.NullValidationResult;
            }

            var result = new ValidationResult();

            // Code should match the pattern
            IVatValidationService.ValidateRegex(result, number, _validationPattern, vatNumber.Number, _validationPatternDescription);

            // Remove special letters
            number = number.Trim('j').Trim('c');

            // Should be consisting of numbers to check checksum
            number.ValidateCustomChecksum(result, (number, result) => CalculateChecksum(number.Substring(0, number.Length - 1)) != number[number.Length - 1]);

            return result;
        }

        private static char CalculateChecksum(string number)
        {
            var s = 0;
            var weights = new int[] { 3, 7, 13, 17, 19, 23, 29, 37, 41, 43, 47, 53, 59, 67, 71 };

            var charArray = number.ToCharArray();
            Array.Reverse(charArray);
            number = new string(charArray);

            for (var i = 0; i < number.Length; i++)
            {
                s = s + weights[i] * (int)char.GetNumericValue(number[i]);
            }

            s %= 11;

            return "01987654321"[s];
        }
    }
}
