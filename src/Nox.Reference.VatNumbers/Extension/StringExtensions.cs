using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Models;
using System.Text;

namespace Nox.Reference.VatNumbers.Extension
{
    internal static class StringExtensions
    {
        public static void ValidateCustomChecksum(this string vatNumber, ValidationResult result, Func<string, ValidationResult, bool> checksumFunc)
        {
            if (!vatNumber.All(x => char.IsDigit(x)))
            {
                result.ValidationErrors.Add(ValidationErrors.NumberShouldConsistOfDigits);
            }
            else
            {
                // Checksum should be valid
                bool isValid = checksumFunc(vatNumber, result);
                if (!isValid)
                {
                    result.ValidationErrors.Add(ValidationErrors.ChecksumError);
                }
            }
        }

        public static void ValidateLuhnDigitForVatNumber(this string vatNumber, ValidationResult result)
        {
            vatNumber.ValidateCustomChecksum(result, (vatNumber, result) => vatNumber.CheckLuhnDigit());
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

        public static bool CheckLuhnDigit(this string stringDigits)
        {
            var lastDigit = (int)char.GetNumericValue(stringDigits[stringDigits.Length - 1]);
            stringDigits = stringDigits.Substring(0, stringDigits.Length - 1);
            var digits = stringDigits.Select(c => (int)char.GetNumericValue(c)).ToList();
            int[] results = { 0, 2, 4, 6, 8, 1, 3, 5, 7, 9 };
            var i = 0;
            var lengthMod = digits.Count % 2;
            return lastDigit == (digits.Sum(d => i++ % 2 == lengthMod ? d : results[d]) * 9) % 10;
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
