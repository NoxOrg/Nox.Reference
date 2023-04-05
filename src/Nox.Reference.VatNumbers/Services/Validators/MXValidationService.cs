using Nox.Reference.Abstractions.VatNumbers;
using Nox.Reference.Shared;
using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;

namespace Nox.Reference.VatNumbers.Services.Validators
{
    // Verifiend manually: FALSE
    // Personal data cleaned: TRUE
    internal class MXValidationService : VatValidationServiceBase
    {
        // TODO: review
        private const string _validationPattern = "^MX[A-Z&Ñ]{3}[0-9]{6}[0-9A-Z]{3}$";
        private const string _validationPatternDescription = "VAT should have 3 letters and after that 6 numbers and after that 3 numbers or letters.";

        private const string _validationPattern2 = @"^[1-9A-V][1-9A-Z][0-9A]$";
        private const string _validationPatternDescription2 = "VAT should consist of three parts. First is number from 1 to 9 or letters from A to V. Second is numbers from 1 to 9 or letters from A to Z. And third is a 'A' letter or a number.";

        public override ValidationResult ValidateVatNumber(IVatNumberInfo vatNumber)
        {
            // Cannot have special characters and remove optional prefix
            var number = vatNumber.NormalizeVatNumber();

            if (string.IsNullOrWhiteSpace(number))
            {
                return ValidationResults.NullValidationResult;
            }

            var result = new ValidationResult();

            var minimumLengthRequirement = 12;
            if (number.Length < minimumLengthRequirement)
            {
                result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLengthRequirement));
                return result;
            }

            // Code should match the pattern
            result.ValidationErrors.AddRange(ValidateRegex(number, _validationPattern, vatNumber.FormattedVatNumber, _validationPatternDescription));

            if (!HasValidDate(number.Substring(3)))
            {
                result.ValidationErrors.Add(ValidationErrors.MX_InvalidDate);
            }

            // Code should match the pattern
            result.ValidationErrors.AddRange(ValidateRegex(number.Substring(number.Length - 3), _validationPattern2, vatNumber.FormattedVatNumber, _validationPatternDescription2));

            // Should be consisting of numbers to check checksum

            // Checksum should be valid
            bool isValid = CalculateChecksum(number.Substring(0, number.Length - 1), number[number.Length - 1]);
            if (!isValid)
            {
                result.ValidationErrors.Add(ValidationErrors.ChecksumError);
            }

            return result;
        }

        private static bool CalculateChecksum(string number, char originalCheckDigit)
        {
            string alphabet = "0123456789ABCDEFGHIJKLMN&OPQRSTUVWXYZ Ñ";
            number = "   " + number;
            number = number.Substring(number.Length - 12);


            int sum = 0;
            for (int i = 0; i < number.Length; i++)
            {
                sum += alphabet.IndexOf(number[i]) * (13 - i);
            }

            var checkDigit = alphabet[(11 - sum).Mod(11)];

            return originalCheckDigit == checkDigit;
        }

        private bool HasValidDate(string number)
        {
            try
            {
                var year = int.Parse(number.Substring(0, 2));
                var month = int.Parse(number.Substring(2, 2));
                var day = int.Parse(number.Substring(4, 2));
                var date = new DateTime(1900 + year, month, day);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
