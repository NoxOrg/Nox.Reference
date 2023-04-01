using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Services
{
    // Verifiend manually: FALSE
    // Personal data cleaned: TRUE
    internal class GreatBritainValidationService : VatValidationServiceBase
    {
        public override ValidationResult ValidateVatNumber(IVatNumber vatNumber)
        {
            // Cannot have special characters
            // Can have or not have 'GB' prefix
            var number = vatNumber.NormalizeVatNumber();

            if (string.IsNullOrWhiteSpace(number))
            {
                return ValidationResults.NullValidationResult;
            }

            var result = new ValidationResult();
            var minimumLengthRequirement = 5;
            if (number.Length < minimumLengthRequirement)
            {
                result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLengthRequirement));
                return result;
            }

            // Government department
            if (number.Substring(0, 2) == "GD")
            {
                if (!int.TryParse(number.Substring(2, 3), out var vatAsNumber) ||
                    vatAsNumber >= 500)
                {
                    result.ValidationErrors.Add(ValidationErrors.GB_InvalidGDVat);
                }

                return result;
            }
            // Health authority
            else if (number.Substring(0, 2) == "HA")
            {
                if (!int.TryParse(number.Substring(2, 3), out var vatAsNumber) ||
                    vatAsNumber < 500)
                {
                    result.ValidationErrors.Add(ValidationErrors.GB_InvalidHAVat);
                }

                return result;
            }

            // Should be consisting of numbers to check checksum
            result.ValidationErrors.AddRange(number.ValidateCustomChecksum(CalculateChecksum));

            return result;
        }

        private static List<string> CalculateChecksum(string number)
        {
            var errorMessage = new List<string>();

            var total = 0;
            if (number[0] == '0')
            {
                errorMessage.Add(string.Format(ValidationErrors.WrongFormatErrorTemplate, number, "First character cannot be 0"));
            }

            var exactLengthRequirement = 9;
            if (number.Length != exactLengthRequirement)
            {
                errorMessage.Add(string.Format(ValidationErrors.LengthShouldEqualError, exactLengthRequirement));
            }

            if (errorMessage.Count > 0)
            {
                return errorMessage;
            }

            var multipliers = new int[] { 8, 7, 6, 5, 4, 3, 2 };

            var no = long.Parse(number.Substring(0, 7));

            for (int i = 0; i < 7; i++)
            {
                total += int.Parse(number[i].ToString()) * multipliers[i];
            }

            int cd = total;
            while (cd > 0)
            {
                cd -= 97;
            }

            cd = Math.Abs(cd);
            if (cd == int.Parse(number.Substring(7, 2)) &&
                no < 9990001 &&
                (no < 100000 || no > 999999) &&
                (no < 9490001 || no > 9700000))
            {
                return errorMessage;
            }

            cd = cd >= 55 ? cd - 55 : cd + 42;

            bool isValid = cd == int.Parse(number.Substring(7, 2)) && no > 1000000;
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }
    }
}
