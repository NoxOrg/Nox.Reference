namespace Nox.Reference.Data.World.Helpers;

internal static class ChecksumValidationHelper
{
    /// <summary>
    /// Method to validate checksum and return an array of
    /// validation errors
    /// </summary>
    /// <param name="vatNumber">Vat number as string</param>
    /// <param name="checksumFunc">Function that validates vat number checksum and returns validation errors</param>
    /// <returns>A enumerable of validation errors</returns>
    public static IEnumerable<string> ValidateCustomChecksum(string vatNumber, Func<string, IEnumerable<string>> checksumFunc)
    {
        var errorMessages = new List<string>();

        // Checksum should be valid
        errorMessages.AddRange(checksumFunc(vatNumber));
        bool isValid = errorMessages.Count == 0;
        if (!isValid &&
            !errorMessages.Contains(ValidationErrors.ChecksumError))
        {
            errorMessages.Add(ValidationErrors.ChecksumError);
        }

        return errorMessages;
    }

    public static IEnumerable<string> ValidateLuhnDigitNumber(string vatNumber)
    {
        return ValidateCustomChecksum(vatNumber, (vatNumber) => CheckLuhnDigit(vatNumber));
    }

    public static IEnumerable<string> ValidateModAndSubstract(string vatNumber, int modulus, int[] weights, int checksumDigitPosition)
    {
        return ValidateCustomChecksum(vatNumber, (vatNumber) => CheckModAndSubstract(vatNumber, modulus, weights, checksumDigitPosition));
    }

    public static IEnumerable<string> ValidateMod(string vatNumber, int modulus, int[] weights, int checksumDigitPosition)
    {
        return ValidateCustomChecksum(vatNumber, (vatNumber) => CheckMod(vatNumber, modulus, weights, checksumDigitPosition));
    }

    public static IEnumerable<string> CheckLuhnDigit(string stringDigits)
    {
        var errorMessage = new List<string>();

        var lastDigit = (int)char.GetNumericValue(stringDigits[stringDigits.Length - 1]);
        var vatNumber = stringDigits.Substring(0, stringDigits.Length - 1);
        var digits = vatNumber.Select(c => (int)char.GetNumericValue(c)).ToList();
        int[] results = { 0, 2, 4, 6, 8, 1, 3, 5, 7, 9 };
        var i = 0;
        var lengthMod = digits.Count % 2;
        var isValid = lastDigit == digits.Sum(d => i++ % 2 == lengthMod ? d : results[d]) * 9 % 10;

        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.LuhnDigitChecksumValidationError);
        }

        return errorMessage;
    }

    private static IEnumerable<string> CheckModAndSubstract(string stringDigits, int modulus, int[] weights, int checksumDigitPosition)
    {
        var errorMessage = new List<string>();

        var sum = 0;
        if (checksumDigitPosition < 0)
        {
            checksumDigitPosition = stringDigits.Length - Math.Abs(checksumDigitPosition);
        }

        // TODO: think about proper handling cases of different length
        // TODO: possibly change way to operate with checksum digit position
        for (var index = 0; index < weights.Count() && index < checksumDigitPosition; index++)
        {
            if (stringDigits.Length <= index)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
                break;
            }

            var digit = int.Parse(stringDigits[index].ToString());
            sum += int.Parse(weights[index].ToString()) * digit;
        }

        if (checksumDigitPosition < 0)
        {
            checksumDigitPosition = stringDigits.Length - Math.Abs(checksumDigitPosition);
        }
        var checkDigit = int.Parse(stringDigits[checksumDigitPosition].ToString());
        var checksum = modulus - sum % modulus;
        if (checksum > 9)
        {
            checksum = 0;
        }

        bool isValid = checksum == checkDigit;
        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
        }

        return errorMessage;
    }

    private static IEnumerable<string> CheckMod(string stringDigits, int modulus, int[] weights, int checksumDigitPosition)
    {
        var errorMessage = new List<string>();

        var sum = 0;
        if (checksumDigitPosition < 0)
        {
            checksumDigitPosition = stringDigits.Length - Math.Abs(checksumDigitPosition);
        }

        // TODO: think about proper handling cases of different length
        // TODO: possibly change way to operate with checksum digit position
        for (var index = 0; index < weights.Length && index < checksumDigitPosition; index++)
        {
            if (stringDigits.Length <= index)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
                break;
            }

            var digit = int.Parse(stringDigits[index].ToString());
            sum += int.Parse(weights[index].ToString()) * digit;
        }
        var checkDigit = int.Parse(stringDigits[checksumDigitPosition].ToString());
        var checksum = sum % modulus;
        if (checksum > 9)
        {
            checksum = 0;
        }

        bool isValid = checksum == checkDigit;
        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
        }

        return errorMessage;
    }
}