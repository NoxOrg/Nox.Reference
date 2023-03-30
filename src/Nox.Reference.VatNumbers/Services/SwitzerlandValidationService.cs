using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Services
{
    // Verifiend manually: FALSE
    // Personal data cleaned: TRUE
    internal class SwitzerlandValidationService : IVatValidationService
    {
        private const string _validationPattern = @"^e?[0-9]{9}(mwst|iva|tva)?$";
        private const string _validationPatternDescription = "VAT should have an optional E letter in front, 9 numeric characters and (mwst|iva|tva) after them.";

        public ValidationResult ValidateVatNumber(IVatNumber vatNumber)
        {
            // Cannot have special characters
            // Can have or not have 'ua' prefix
            var number = vatNumber.NormalizeVatNumber();
            number = number.Replace("che-", string.Empty);

            if (string.IsNullOrWhiteSpace(number))
            {
                return ValidationResults.NullValidationResult;
            }

            var result = new ValidationResult();
            
            // Code should match the pattern
            IVatValidationService.ValidateRegex(result, number, _validationPattern, vatNumber.Number, _validationPatternDescription);
            number = number.RemoveCharacters();

            // Should be consisting of numbers to check checksum
            number.ValidateCustomChecksum(result, CalculateChecksum);

            return result;
        }

        private static bool CalculateChecksum(string number, ValidationResult result)
        {
            var minimumLengthRequirement = 9;
            if (number.Length < minimumLengthRequirement)
            {
                result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLengthRequirement));
                return false;
            }

            var sum = 0;
            int[] weight = new int[] { 5, 4, 3, 2, 7, 6, 5, 4 };
            for (var i = 0; i < 8; i++)
            {
                sum += (int)(char.GetNumericValue(number[i])) * weight[i];
            }

            sum = 11 - sum % 11;
            if (sum == 10)
            {
                result.ValidationErrors.Add(ValidationErrors.ChecksumError);
            }
            if (sum == 11)
            {
                sum = 0;
            }

            return sum.ToString() == number.Substring(8, 1);
        }
    }
}
