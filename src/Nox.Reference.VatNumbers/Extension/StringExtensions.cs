using Nox.Reference.VatNumbers.Constants;
using System.Text;

namespace Nox.Reference.VatNumbers.Extension
{
    internal static class StringExtensions
    {
        public static List<string> ValidateCustomChecksum(this string vatNumber, Func<string, List<string>> checksumFunc)
        {
            var errorMessages = new List<string>();

            if (!vatNumber.All(x => char.IsDigit(x)))
            {
                errorMessages.Add(ValidationErrors.NumberShouldConsistOfDigits);
            }
            else
            {
                // Checksum should be valid
                errorMessages.AddRange(checksumFunc(vatNumber));
                bool isValid = errorMessages.Count == 0;
                if (!isValid &&
                    !errorMessages.Contains(ValidationErrors.ChecksumError))
                {
                    errorMessages.Add(ValidationErrors.ChecksumError);
                }
            }

            return errorMessages;
        }

        public static List<string> ValidateLuhnDigitForVatNumber(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckLuhnDigit());
        }

        public static string RemoveSpecialCharacthers(this string vatNumber)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < vatNumber?.Length; i++)
            {
                if (char.IsLetterOrDigit(vatNumber[i]))
                {
                    sb.Append(vatNumber[i]);
                }
            }

            return sb.ToString();
        }

        public static string RemoveCharacters(this string vatNumber)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < vatNumber?.Length; i++)
            {
                if (char.IsDigit(vatNumber[i]))
                {
                    sb.Append(vatNumber[i]);
                }
            }

            return sb.ToString();
        }

        public static List<string> CheckLuhnDigit(this string stringDigits)
        {
            var errorMessage = new List<string>();

            var lastDigit = (int)char.GetNumericValue(stringDigits[stringDigits.Length - 1]);
            stringDigits = stringDigits.Substring(0, stringDigits.Length - 1);
            var digits = stringDigits.Select(c => (int)char.GetNumericValue(c)).ToList();
            int[] results = { 0, 2, 4, 6, 8, 1, 3, 5, 7, 9 };
            var i = 0;
            var lengthMod = digits.Count % 2;
            var isValid = lastDigit == (digits.Sum(d => i++ % 2 == lengthMod ? d : results[d]) * 9) % 10;
            
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.LuhnDigitChecksumValidationError);
            }

            return errorMessage;
        }

        public static int GetSumOfDigitsMulipliedByMultipliers(this string input, int[] multipliers, int start = 0)
        {
            var sum = 0;

            for (var index = start; index < multipliers.Length; index++)
            {
                var digit = multipliers[index];
                sum += int.Parse(input[index].ToString()) * digit;
            }

            return sum;
        }
    }
}
