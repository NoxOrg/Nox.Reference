using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Services
{
    // Verifiend manually: FALSE
    // Personal data cleaned: TRUE
    internal class SwitzerlandValidationService : VatValidationServiceBase
    {
        private const string _validationPattern = @"^E?[0-9]{9}(MWST|IVA|TVA)?$";
        private const string _validationPatternDescription = "VAT should have an optional E letter in front, 9 numeric characters and (MWST|IVA|TVA) after them.";

        public override ValidationResult ValidateVatNumber(IVatNumber vatNumber)
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
            result.ValidationErrors.AddRange(ValidateRegex(number, _validationPattern, vatNumber.Number, _validationPatternDescription));
            number = number.RemoveCharacters();

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

            var sum = 0;
            int[] weight = new int[] { 5, 4, 3, 2, 7, 6, 5, 4 };
            for (var i = 0; i < 8; i++)
            {
                sum += (int)(char.GetNumericValue(number[i])) * weight[i];
            }

            sum = 11 - sum % 11;
            if (sum == 10)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
                return errorMessage;
            }

            if (sum == 11)
            {
                sum = 0;
            }

            var isValid = sum.ToString() == number.Substring(8, 1);
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }
    }
}
