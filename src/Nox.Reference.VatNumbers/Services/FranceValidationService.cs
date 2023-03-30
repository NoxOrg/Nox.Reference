using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Services
{
    // Verifiend manually: FALSE
    // Personal data cleaned: TRUE
    internal class FranceValidationService : IVatValidationService
    {
        // TODO: this alghorym is very weird, need to review it and possibly rewrite from scratch
        private static string _alphabet = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZ";
        private const string _validationPattern = @"^\d{11}$";
        private const string _validationPatternDescription = "VAT should have 11 numeric characters and optional 'FR' text in upper or lower case front of them.";

        public ValidationResult ValidateVatNumber(IVatNumber vatNumber)
        {
            // Cannot have special characters
            // Can have or not have 'fr' prefix
            var number = vatNumber.NormalizeVatNumber();

            if (string.IsNullOrWhiteSpace(number))
            {
                return ValidationResults.NullValidationResult;
            }

            var result = new ValidationResult();

            // Code should match the pattern
            IVatValidationService.ValidateRegex(result, number, _validationPattern, vatNumber.Number, _validationPatternDescription);

            var exactLengthRequirement = 11;
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
        }

        private static bool CalculateChecksum(string number, ValidationResult result)
        {
            if (number.Substring(2, 3) != "000")
            {
                return number.CheckLuhnDigit();
            }
            else if (!number.All(char.IsDigit))
            {
                if (int.Parse(number.Substring(0, 2)) != (long.Parse(number.Substring(2) + "12") % 97))
                {
                    return false;
                }
            }
            else
            {
                int check = 0;
                if (char.IsDigit(number[0]))
                {
                    check =
                        (_alphabet.IndexOf(number[0]) * 24) +
                        _alphabet.IndexOf(number[1]) - 10;
                }
                else
                {
                    check = (
                        _alphabet.IndexOf(number[0]) * 34 +
                        _alphabet.IndexOf(number[1]) - 100);
                }

                if ((long.Parse(number.Substring(2)) + 1 + check / 11) % 11 != (check % 11))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
