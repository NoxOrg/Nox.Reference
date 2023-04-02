using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Services
{
    // Verifiend manually: FALSE
    // Personal data cleaned: TRUE
    internal class BelgiumValidationService : VatValidationServiceBase
    {
        // TODO: review
        private const string _validationPattern = @"^0?\d{9}$";
        private const string _validationPatternDescription = "VAT should have 10 numeric characters";

        public override ValidationResult ValidateVatNumber(IVatNumber vatNumber)
        {
            // Cannot have special characters
            // Can have or not have 'BE' prefix
            var number = vatNumber.NormalizeVatNumber();
            if (number.Length == 9)
            {
                number = number.PadLeft(10, '0');
            }

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

            var isValid = 97 - int.Parse(number.Substring(0, 8)) % 97 == int.Parse(number.Substring(8, 2));
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }
    }
}
