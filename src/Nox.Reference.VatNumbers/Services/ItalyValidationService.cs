using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Services
{
    // Verifiend manually: FALSE
    // Personal data cleaned: TRUE
    internal class ItalyValidationService : IVatValidationService
    {
        private const string _validationPattern = @"^\d{11}$";
        private const string _validationPatternDescription = "VAT should consist of 11 numeric characters.";

        public ValidationResult ValidateVatNumber(IVatNumber vatNumber)
        {
            // Cannot have special characters
            // Can have or not have 'it' prefix
            var number = vatNumber.NormalizeVatNumber();

            if (string.IsNullOrWhiteSpace(number))
            {
                return ValidationResults.NullValidationResult;
            }

            var result = new ValidationResult();

            // Code should match the pattern
            IVatValidationService.ValidateRegex(result, number, _validationPattern, vatNumber.Number, _validationPatternDescription);

            // Should be consisting of numbers to check checksum
            number.ValidateCustomChecksum(result, CalculateChecksum);

            return result;
        }

        private static bool CalculateChecksum(string number, ValidationResult result)
        {
            var minimumLengthRequirement = 11;
            if (number.Length != minimumLengthRequirement)
            {
                result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLengthRequirement));
                return false;
            }
            
            int[] Multipliers = { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };

            var res = long.Parse(number);
            if (res == 0)
            {
                result.ValidationErrors.Add(string.Format(ValidationErrors.WrongFormatErrorTemplate, number, "Cannot have 7 zeroes as first 7 digits."));
                return false;
            }

            var temp = int.Parse(number.Substring(7, 3));

            if ((temp < 1 || temp > 201) && temp != 999 && temp != 888)
            {
                return false;
            }

            var index = 0;
            var sum = 0;
            foreach (var m in Multipliers)
            {
                temp = int.Parse(number[index++].ToString()) * m;
                sum += temp > 9
                    ? (int)Math.Floor(temp / 10D) + temp % 10
                    : temp;
            }

            var checkDigit = 10 - sum % 10;

            if (checkDigit > 9)
            {
                checkDigit = 0;
            }

            return checkDigit == int.Parse(number[10].ToString());
        }
    }
}
