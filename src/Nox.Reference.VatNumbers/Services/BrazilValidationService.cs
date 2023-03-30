using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Services
{
    // Verifiend manually: FALSE
    // Personal data cleaned: TRUE
    internal class BrazilValidationService : IVatValidationService
    {
        // TODO: review
        private const string _validationPattern = @"^\d{14}$";
        private const string _validationPatternDescription = "VAT should have 14 numeric characters";

        public ValidationResult ValidateVatNumber(IVatNumber vatNumber)
        {
            // Cannot have special characters
            // Can have or not have 'BR' prefix
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
            var minimumLengthRequirement = 14;
            if (number.Length < minimumLengthRequirement)
            {
                result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLengthRequirement));
                return false;
            }

            var registration = number.Substring(0, 12);
            registration += DigitChecksum(registration);
            registration += DigitChecksum(registration);


            return registration.Substring(registration.Length - 2) == number.Substring(registration.Length - 2);
        }

        private static int DigitChecksum(string numbers)
        {
            int index = 2;

            char[] charArray = numbers.ToCharArray();
            Array.Reverse(charArray);
            numbers = new string(charArray);

            int sum = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                sum += int.Parse(numbers[i].ToString()) * index;

                index = index == 9 ? 2 : index + 1;
            }

            var mod = sum % 11;

            return mod < 2 ? 0 : 11 - mod;
        }
    }
}
