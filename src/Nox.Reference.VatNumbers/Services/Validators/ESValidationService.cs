using Nox.Reference.Abstractions.VatNumbers;
using Nox.Reference.Shared;
using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using System.Text.RegularExpressions;

namespace Nox.Reference.VatNumbers.Services.Validators
{
    // Verifiend manually: FALSE
    // Personal data cleaned: TRUE
    internal class ESValidationService : VatValidationServiceBase
    {
        public override ValidationResult ValidateVatNumber(IVatNumberInfo vatNumber)
        {
            // Cannot have special characters and remove optional prefix
            var number = vatNumber.NormalizeVatNumber();
            number = number.ToUpper();

            if (string.IsNullOrWhiteSpace(number))
            {
                return ValidationResults.NullValidationResult;
            }

            var result = new ValidationResult();

            var exactLengthRequirement = 9;
            if (number.Length != exactLengthRequirement)
            {
                result.ValidationErrors.Add(string.Format(ValidationErrors.LengthShouldEqualError, exactLengthRequirement));
            }
            else
            {
                // Should be consisting of numbers to check checksum
                result.ValidationErrors.AddRange(CalculateChecksum(number));
            }

            return result;
        }

        private static List<string> CalculateChecksum(string number)
        {
            var errorMessages = new List<string>();
            var total = 0;
            var multipliers = new int[] { 2, 1, 2, 1, 2, 1, 2 };

            int temp;
            if (Regex.IsMatch(number, @"^[A-H|J|U|V]\d{8}$"))
            {
                for (int i = 0; i < 7; i++)
                {
                    temp = int.Parse(number[i + 1].ToString()) * multipliers[i];
                    if (temp > 9)
                    {
                        total += (int)Math.Floor((decimal)temp / 10) + temp % 10;
                    }
                    else
                    {
                        total += temp;
                    }
                }
                total = 10 - total % 10;
                if (total == 10) { total = 0; }

                bool isValid = total == int.Parse(number.Substring(8, 1));
                if (!isValid)
                {
                    errorMessages.Add(ValidationErrors.ChecksumError);
                }
            }
            else if (Regex.IsMatch(number, @"^[A-H|N-S|W]\d{7}[A-J]$"))
            {
                for (int i = 0; i < 7; i++)
                {
                    temp = int.Parse(number[i + 1].ToString()) * multipliers[i];
                    if (temp > 9)
                    {
                        total += (int)Math.Floor((decimal)temp / 10) + temp % 10;
                    }
                    else
                    {
                        total += temp;
                    }
                }

                total = 10 - total % 10;
                char totalChar = (char)(total + 64);

                bool isValid = totalChar == number[8];
                if (!isValid)
                {
                    errorMessages.Add(ValidationErrors.ChecksumError);
                }
            }
            else if (Regex.IsMatch(number, @"^[0-9|Y|Z]\d{7}[A-Z]$"))
            {
                var tempnumber = number;
                if (tempnumber[0] == 'Y')
                {
                    tempnumber = tempnumber.Replace("Y", "1");
                }
                else if (tempnumber[0] == 'Z')
                {
                    tempnumber = tempnumber.Replace("Z", "2");
                }

                bool isValid = tempnumber[8] == "TRWAGMYFPDXBNJZSQVHLCKE"[int.Parse(tempnumber.Substring(0, 8)) % 23];
                if (!isValid)
                {
                    errorMessages.Add(ValidationErrors.ChecksumError);
                }
            }
            else if (Regex.IsMatch(number, @"^[K|L|M|X]\d{7}[A-Z]$"))
            {
                bool isValid = number[8] == "TRWAGMYFPDXBNJZSQVHLCKE"[int.Parse(number.Substring(1, 7)) % 23];
                if (!isValid)
                {
                    errorMessages.Add(ValidationErrors.ChecksumError);
                }
            }
            else
            {
                errorMessages.Add(ValidationErrors.UnknownFormat);
            }

            return errorMessages;
        }
    }
}
