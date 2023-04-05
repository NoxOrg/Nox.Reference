using Nox.Reference.Abstractions.VatNumbers;
using Nox.Reference.Shared;
using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;

namespace Nox.Reference.VatNumbers.Services.Validators
{
    // Verifiend manually: FALSE
    // Personal data cleaned: TRUE
    internal class PTValidationService : VatValidationServiceBase
    {
        public override ValidationResult ValidateVatNumber(IVatNumberInfo vatNumber)
        {
            // Cannot have special characters and remove optional prefix
            if (string.IsNullOrWhiteSpace(vatNumber.FormattedVatNumber))
            {
                return ValidationResults.NullValidationResult;
            }

            var result = new ValidationResult();

            // Code should match the pattern
            result.ValidationErrors.AddRange(ValidateRegex(vatNumber.FormattedVatNumber, vatNumber.ValidationRegex, vatNumber.OriginalVatNumber, vatNumber.ValidationFormatDescription));

            // Should be consisting of numbers to check checksum
            result.ValidationErrors.AddRange(vatNumber.FormattedVatNumber.Substring(2).ValidateCustomChecksum(CalculateChecksum));

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
