using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Services
{
    // Verifiend manually: FALSE
    // Personal data cleaned: TRUE
    internal class ColombiaValidatorService : VatValidationServiceBase
    {
        private const string _validationPattern = @"^[CJ]{0,1}[0-9]{9,10}[CJ]{0,1}$";
        private const string _validationPatternDescription = "VAT should have 9 or 10 numeric characters optionally with a letter C or J before or after the numeric code.";

        private static int[] _weights = new int[] { 3, 7, 13, 17, 19, 23, 29, 37, 41, 43, 47, 53, 59, 67, 71 };

        public override ValidationResult ValidateVatNumber(IVatNumber vatNumber)
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
            result.ValidationErrors.AddRange(ValidateRegex(number, _validationPattern, vatNumber.Number, _validationPatternDescription));

            // Remove special letters
            number = number.Trim('J').Trim('C');

            var minimumLengthRequirement = 9;
            var maximimLengthRequirement = _weights.Length;
            if (number.Length < minimumLengthRequirement)
            {
                result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLengthRequirement));
            }
            else if (number.Length > maximimLengthRequirement)
            {
                result.ValidationErrors.Add(string.Format(ValidationErrors.MaximumNumbericLengthError, minimumLengthRequirement));
            }
            else
            {
                // Should be consisting of numbers to check checksum
                result.ValidationErrors.AddRange(number.ValidateCustomChecksum(CalculateChecksum));
            }

            return result;
        }

        // TODO: this causes a lot of failures
        private static List<string> CalculateChecksum(string number)
        {
            var lastDigit = number[number.Length - 1];
            number = number.Substring(0, number.Length - 1);

            var errorMessage = new List<string>();

            var s = 0;

            var charArray = number.ToCharArray();
            Array.Reverse(charArray);
            number = new string(charArray);

            for (var i = 0; i < number.Length; i++)
            {
                s += _weights[i] * (int)char.GetNumericValue(number[i]);
            }

            s %= 11;

            var checkDigit = "01987654321"[s];
            var isValid = checkDigit == lastDigit;
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }
    }
}
