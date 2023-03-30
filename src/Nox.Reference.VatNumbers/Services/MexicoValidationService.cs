using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using Nox.Reference.VatNumbers.Models;
using System.Text.RegularExpressions;

namespace Nox.Reference.VatNumbers.Services
{
    // Verifiend manually: FALSE
    // Personal data cleaned: TRUE
    internal class MexicoValidationService : IVatValidationService
    {
        // TODO: review
        private const string _validationPattern = @"^[1-9]\d{8}$";
        private const string _validationPatternDescription = "VAT should have 9 numeric characters first of which is not 0.";

        public ValidationResult ValidateVatNumber(IVatNumber vatNumber)
        {
            // Cannot have special characters
            // Can have or not have 'DE' prefix
            var number = vatNumber.NormalizeVatNumber();

            if (string.IsNullOrWhiteSpace(number))
            {
                return ValidationResults.NullValidationResult;
            }

            var result = new ValidationResult();

            // Code should match the pattern
            IVatValidationService.ValidateRegex(result, number, _validationPattern, vatNumber.Number, _validationPatternDescription);

            var exactLengthRequirement = 9;
            if (number.Length != exactLengthRequirement)
            {
                result.ValidationErrors.Add(string.Format(ValidationErrors.LengthShouldEqualError, exactLengthRequirement));
            }
            else
            {
                // Should be consisting of numbers to check checksum
                number.ValidateCustomChecksum(result, CalculateChecksum);
            }

            return result;


            rfc = rfc.RemoveSpecialCharacthers();

            if (rfc.Length == 12) //# number assigned to company
            {
                if (!Regex.IsMatch(rfc, "^[A-Z&Ñ]{3}[0-9]{6}[0-9A-Z]{3}$"))
                {
                    return ValidationResult.Invalid("Invalid format");
                }
                else if (!HasValidDate(rfc))
                {
                    return ValidationResult.InvalidDate();
                }
            }
            else
            {
                return ValidationResult.InvalidLength();
            }

            if (rfc.Length >= 12)
            {
                if (!Regex.IsMatch(rfc.Substring(rfc.Length - 3), @"^[1-9A-V][1-9A-Z][0-9A]$"))
                {
                    return ValidationResult.Invalid("Invalid");
                }
                else if (rfc[rfc.Length - 1] != CalculateChecksum(rfc.Substring(0, rfc.Length - 1)))
                {
                    return ValidationResult.InvalidChecksum();
                }
            }
            return ValidationResult.Success();
        }

        private static bool CalculateChecksum(string number, ValidationResult result)
        {
            var product = 10;
            for (var index = 0; index < 8; index++)
            {
                var sum = (int.Parse(number[index].ToString()) + product) % 10;
                if (sum == 0)
                {
                    sum = 10;
                }

                product = 2 * sum % 11;
            }

            var val = 11 - product;
            var checkDigit = val == 10
                ? 0
                : val;

            return checkDigit == int.Parse(number[8].ToString());
        }
    }
}
