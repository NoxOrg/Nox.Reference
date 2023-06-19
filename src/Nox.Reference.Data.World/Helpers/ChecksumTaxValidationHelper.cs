using AutoMapper.Execution;
using Nox.Reference.Common;
using System.Text;
using YamlDotNet.Core.Tokens;

namespace Nox.Reference.Data.World.Helpers;

internal static class ChecksumTaxValidationHelper
{
    public static IEnumerable<string> ValidateTaxCHAlgorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckCHAlgorithm(vatNumber));
    }

    public static IEnumerable<string> ValidateTaxBRAlgorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckBRAlgorithm(vatNumber));
    }

    public static IEnumerable<string> ValidateTaxITAlgorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckITAlgorithm(vatNumber));
    }

    public static IEnumerable<string> ValidateTaxDEAlgorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckDEAlgorithm(vatNumber));
    }

    public static IEnumerable<string> ValidateTaxNLAlgorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckNLAlgorithm(vatNumber));
    }

    public static IEnumerable<string> ValidateTaxMX1Algorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckMX1Algorithm(vatNumber));
    }

    public static IEnumerable<string> ValidateTaxMX2Algorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckMX2Algorithm(vatNumber));
    }

    public static IEnumerable<string> ValidateTaxINAlgorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckINAlgorithm(vatNumber));
    }

    public static IEnumerable<string> ValidateTaxCAAlgorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckCAAlgorithm(vatNumber));
    }

    public static IEnumerable<string> ValidateTaxBEAlgorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckBEAlgorithm(vatNumber));
    }

    public static IEnumerable<string> ValidateTaxAUAlgorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckAUAlgorithm(vatNumber));
    }

    public static IEnumerable<string> ValidateTaxPLAlgorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckPLAlgorithm(vatNumber));
    }

    public static IEnumerable<string> ValidateTaxDKAlgorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckDKAlgorithm(vatNumber));
    }

    public static IEnumerable<string> ValidateTaxATAlgorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckATAlgorithm(vatNumber));
    }

    public static IEnumerable<string> ValidateTaxTRAlgorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckTRAlgorithm(vatNumber));
    }

    public static IEnumerable<string> ValidateTaxSEAlgorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckSEAlgorithm(vatNumber));
    }

    public static IEnumerable<string> ValidateTaxILAlgorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckILAlgorithm(vatNumber));
    }

    public static IEnumerable<string> ValidateTaxNOAlgorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckNOAlgorithm(vatNumber));
    }

    public static IEnumerable<string> ValidateTaxNZAlgorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckNZAlgorithm(vatNumber));
    }

    public static IEnumerable<string> ValidateTaxFIAlgorithm(string vatNumber)
    {
        return ChecksumValidationHelper.ValidateCustomChecksum(vatNumber, (vatNumber) => CheckFIAlgorithm(vatNumber));
    }

    private static IEnumerable<string> CheckCHAlgorithm(string stringDigits)
    {
        var errorMessage = new List<string>();

        // TODO: possible date check
        //if (!ChecksumValidationHelper.HasValidDate(stringDigits.Substring(0, 6)))
        //{
        //    errorMessage.Add(ValidationErrors.InvalidDate);
        //}

        var sum = 0;
        for (var i = 0; i < 12; i += 1)
        {
            if (i % 2 == 0)
            {
                sum += int.Parse(stringDigits[i].ToString());
            }
            else
            {
                sum += int.Parse(stringDigits[i].ToString()) * 3;
            }
        }

        var expectedCheckDigit = 0;
        if (sum % 10 != 0)
        {
            var roundTen = Math.Floor((decimal)sum / 10) * 10 + 10;
            expectedCheckDigit = (int)roundTen - sum;
        }

        var checkSum = expectedCheckDigit.ToString();
        var checkDigit = stringDigits.Substring(12, 1);
        var isValid = checkSum == checkDigit;
        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
        }

        return errorMessage;
    }

    private static IEnumerable<string> CheckBRAlgorithm(string stringDigits)
    {
        var errorMessage = new List<string>();

        int sum = 0;

        for (var i = 1; i <= 9; i++)
        {
            sum = sum + int.Parse(stringDigits.Substring(i - 1, 1)) * (11 - i);
        }
        int rest = sum * 10 % 11;

        if ((rest == 10) || (rest == 11))
            rest = 0;

        if (rest != int.Parse(stringDigits.Substring(9, 1)))
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
            return errorMessage;
        }

        sum = 0;
        for (int i = 1; i <= 10; i++)
            sum = sum + int.Parse(stringDigits.Substring(i - 1, 1)) * (12 - i);

        rest = (sum * 10) % 11;

        if ((rest == 10) || (rest == 11))
            rest = 0;

        var checkSum = rest;
        var checkDigit = int.Parse(stringDigits.Substring(10, 1));
        var isValid = checkSum == checkDigit;
        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
        }

        return errorMessage;
    }

    private static IEnumerable<string> CheckITAlgorithm(string stringDigits)
    {
        // TODO: investigate OmocodeChars scenario
        // more info: https://github.com/anghelvalentin/CountryValidator/blob/master/CountryValidator/CountriesValidators/ItalyValidator.cs

        var errorMessage = new List<string>();

        int tot = 0;
        var f15 = stringDigits.Substring(0, 15);

        // TODO: move to static
        int[] ControlCodeArray = new[] { 1, 0, 5, 7, 9, 13, 15, 17, 19, 21, 2, 4, 18, 20, 11, 3, 6, 8, 12, 14, 16, 10, 22, 25, 24, 23 };

        byte[] arrCode = Encoding.UTF8.GetBytes(f15.ToUpper());
        for (int i = 0; i < f15.Length; i++)
        {
            if ((i + 1) % 2 == 0) tot += (char.IsLetter(f15, i))
                ? arrCode[i] - (byte)'A'
                : arrCode[i] - (byte)'0';
            else tot += (char.IsLetter(f15, i))
                ? ControlCodeArray[(arrCode[i] - (byte)'A')]
                : ControlCodeArray[(arrCode[i] - (byte)'0')];
        }
        tot %= 26;
        char l = (char)(tot + 'A');

        var checkSum = l;
        var checkDigit = stringDigits.Substring(15, 1);
        var isValid = checkSum.ToString() == checkDigit;
        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
        }

        return errorMessage;
    }

    private static IEnumerable<string> CheckDEAlgorithm(string stringDigits)
    {
        var errorMessage = new List<string>();

        char[] digits = stringDigits.ToCharArray();
        var first10Digits = digits.Take(10);

        var counts = first10Digits.GroupBy(x => x)
              .Select(g => new { Value = g.Key, Count = g.Count() }).ToList();

        if (counts.Count != 9 && counts.Count != 8)
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
            return errorMessage;
        }
        int sum = 0;
        int product = 10;
        for (int i = 0; i <= 9; i++)
        {
            sum = (int)(char.GetNumericValue(digits[i]) + product) % 10;
            if (sum == 0)
            {
                sum = 10;
            }
            product = (sum * 2) % 11;
        }
        int checksum = 11 - product;
        if (checksum == 10)
        {
            checksum = 0;
        }

        var checkDigit = stringDigits.Substring(10, 1);
        var isValid = checksum.ToString() == checkDigit;
        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
        }

        return errorMessage;
    }

    private static IEnumerable<string> CheckNLAlgorithm(string stringDigits)
    {
        var errorMessage = new List<string>();

        // TODO: try joining with existing one
        int sum = 0;
        for (int i = 0; i < stringDigits.Length - 1; i++)
        {
            sum += (9 - i) * (int)char.GetNumericValue(stringDigits[i]);
        }

        var checksum = (sum - (int)char.GetNumericValue(stringDigits[stringDigits.Length - 1])).Mod(11);

        var checkDigit = stringDigits.Substring(8, 1);
        var isValid = checksum.ToString() == checkDigit;
        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
        }

        return errorMessage;
    }

    private static IEnumerable<string> CheckMX1Algorithm(string stringDigits)
    {
        var errorMessage = new List<string>();

        var isValid = ChecksumValidationHelper.HasValidDate(stringDigits.Substring(0, 6));
        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.InvalidDate);
        }

        return errorMessage;
    }

    private static IEnumerable<string> CheckMX2Algorithm(string stringDigits)
    {
        var errorMessage = new List<string>();

        string alphabet = "0123456789ABCDEFGHIJKLMN&OPQRSTUVWXYZ Ñ";
        stringDigits = ("   " + stringDigits);
        stringDigits = stringDigits.Substring(stringDigits.Length - 12);


        int sum = 0;
        for (int i = 0; i < stringDigits.Length; i++)
        {
            sum += alphabet.IndexOf(stringDigits[i]) * (13 - i);
        }

        var checksum = alphabet[(11 - sum).Mod(11)];

        var checkDigit = stringDigits.Substring(12, 1);
        var isValid = checksum.ToString() == checkDigit;
        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
        }

        return errorMessage;
    }

    private static IEnumerable<string> CheckINAlgorithm(string stringDigits)
    {
        var errorMessage = new List<string>();

        // TODO: move static
        string[] card_holder_types = new string[]{
                "A",// 'Association of Persons (AOP)'
                "B",//Body of Individuals (BOI)'
                "C",//Company'
                "F",//Firm'
                "G",//Government'
                "H",//HUF (Hindu Undivided Family)'
                "L",//Local Authority'
                "J",//Artificial Juridical Person'
                "P",//Individual
                "T",//Trust (AOP)
                "K",//Krish (Trust Krish)
            };

        var isValid = card_holder_types.Contains(stringDigits.Substring(3, 1));
        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.InvalidCardType);
        }

        return errorMessage;
    }

    private static IEnumerable<string> CheckCAAlgorithm(string stringDigits)
    {
        var errorMessage = new List<string>();

        int[] digits = new int[stringDigits.Length];

        var total = digits.Where((value, index) => index % 2 == 0 && index != 8).Sum()
                    + digits.Where((value, index) => index % 2 != 0).Select(v => v * 2)
                          .SelectMany(v => v.ToDigitEnumerable()).Sum();

        var checksum = (10 - (total % 10)) % 10;
        var checkDigit = digits.Last();
        var isValid = checksum == checkDigit;
        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
        }

        return errorMessage;
    }

    private static IEnumerable<string> CheckBEAlgorithm(string stringDigits)
    {
        // TODO: possibly join with existing
        var errorMessage = new List<string>();

        var checkDigit = stringDigits.Substring(stringDigits.Length - 2);

        var nrToCheck = long.Parse(stringDigits.Substring(0, 9));

        if (ModFunction(nrToCheck).ToString() == checkDigit)
        {
            return errorMessage;
        }

        nrToCheck = long.Parse('2' + stringDigits.Substring(0, 9));

        var checksum = ModFunction(nrToCheck).ToString();
        var isValid = checksum == checkDigit;
        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
        }

        return errorMessage;
    }

    private static int ModFunction(long nr)
    {
        return (int)(97 - (nr % 97));
    }

    private static IEnumerable<string> CheckAUAlgorithm(string stringDigits)
    {
        // TODO: possibly join with existing
        var errorMessage = new List<string>();

        int[] weights = new int[] { 1, 4, 3, 7, 5, 8, 6, 9, 10 };
        int sum = 0;
        for (int i = 0; i < stringDigits.Length; i++)
        {
            sum += ((int)char.GetNumericValue(stringDigits[i])) * weights[i];
        }

        var checksum = sum % 11;
        var checkDigit = 0;
        var isValid = checksum == checkDigit;
        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
        }

        return errorMessage;
    }

    #region PL

    private static IEnumerable<string> CheckPLAlgorithm(string stringDigits)
    {
        // TODO: possibly join with existing
        var errorMessage = new List<string>();

        var peselList = stringDigits.ToCharArray().Select(n => (int)char.GetNumericValue(n)).ToList();
        var peselMonth = int.Parse(stringDigits.Substring(2, 2));
        var peselDay = int.Parse(stringDigits.Substring(4, 2));
        var peselYear = ComputePESELYear(Int32.Parse(stringDigits.Substring(0, 2)), peselMonth);

        if (!CheckIfMonthIsCorrect(peselMonth) ||
            !CheckIfDayIsCorrect(peselDay, peselMonth, peselYear))
        {
            errorMessage.Add(ValidationErrors.InvalidDate);
            return errorMessage;
        }

        var checksum = ComputePESELChecksum(peselList);
        var checkDigit = int.Parse(stringDigits.Substring(10, 1));
        var isValid = checksum == checkDigit;
        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
        }

        return errorMessage;
    }

    private static int ComputePESELYear(int PESEL2DigitsYear, int PESELMonth)
    {
        if (PESELMonth > 0 && PESELMonth < 13)
        {
            return 1900 + PESEL2DigitsYear;
        }
        else if (PESELMonth > 20 && PESELMonth < 33)
        {
            return 2000 + PESEL2DigitsYear;
        }
        else if (PESELMonth > 40 && PESELMonth < 53)
        {
            return 2100 + PESEL2DigitsYear;
        }
        else if (PESELMonth > 60 && PESELMonth < 73)
        {
            return 2200 + PESEL2DigitsYear;
        }

        return 1800 + PESEL2DigitsYear;
    }

    private static bool CheckIfMonthIsCorrect(int PESELMonth)
    {
        if (PESELMonth % 20 == 0)
        {
            return false;
        }

        for (int i = 0; i <= 80; i += 20)
        {
            for (int j = 13 + i; j <= 19 + i; j++)
            {
                if (PESELMonth == j)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static bool CheckIfDayIsCorrect(int PESELDay, int PESELMonth, int PESELYear)
    {
        if (PESELDay == 0 || PESELDay > 31)
        {
            return false;
        }

        for (int i = 0; i <= 80; i += 20)
        {
            for (int j = 1 + i; j <= 7 + i ? j <= 7 + i : j <= 12 + i; j += 2)
            {
                if (PESELMonth == j && PESELDay > 31)
                {
                    return false;
                }
            }

            for (int j = 4 + i; j <= 6 + i; j += 2)
            {
                if (PESELMonth == j && PESELDay > 30)
                {
                    return false;
                }
            }

            for (int j = 9 + i; j <= 11 + i; j += 2)
            {
                if (PESELMonth == j && PESELDay > 30)
                {
                    return false;
                }
            }
        }

        if (PESELYear == 2000 || (PESELYear % 4 == 0 && PESELYear % 100 != 0))
        {
            if (PESELMonth % 20 == 2 && PESELDay > 29)
            {
                return false;
            }
        }
        else
        {
            if (PESELMonth % 20 == 2 && PESELDay > 28)
            {
                return false;
            }
        }

        return true;
    }

    private static int ComputePESELChecksum(List<int> PESELList)
    {
        int sum = PESELList[0] * 9 + PESELList[1] * 7 + PESELList[2] * 3 + PESELList[3] * 1 + PESELList[4] * 9 +
                  PESELList[5] * 7 + PESELList[6] * 3 + PESELList[7] * 1 + PESELList[8] * 9 + PESELList[9] * 7;

        return sum % 10;
    }

    #endregion PL

    private static IEnumerable<string> CheckDKAlgorithm(string stringDigits)
    {
        var errorMessage = new List<string>();

        var day = int.Parse(stringDigits.Substring(0, 2));
        var month = int.Parse(stringDigits.Substring(2, 2));
        var year = int.Parse(stringDigits.Substring(4, 2));

        if ("5678".IndexOf(stringDigits[6]) != -1 && year >= 58)
        {
            year += 1800;
        }
        else if ("0123".IndexOf(stringDigits[6]) != -1 || ("49".IndexOf(stringDigits[6]) != -1 && year >= 37))
        {
            year += 1900;
        }
        else
        {
            year += 2000;
        }

        try
        {
            DateTime dateTime = new DateTime(year, month, day);
            if (dateTime > DateTime.UtcNow)
            {
                errorMessage.Add(ValidationErrors.InvalidDate);
                return errorMessage;
            }
        }
        catch
        {
            errorMessage.Add(ValidationErrors.InvalidDate);
            return errorMessage;
        }

        return errorMessage;
    }

    private static IEnumerable<string> CheckATAlgorithm(string stringDigits)
    {
        var errorMessage = new List<string>();

        int sum = 0;
        for (int i = 0; i < 8; i++)
        {
            int n = (int)char.GetNumericValue(stringDigits[i]);
            if (i % 2 != 0)
            {
                sum += (int)char.GetNumericValue("0246813579"[n]);
            }
            else
            {
                sum += n;
            }
        }

        var checksum = (10 - sum).Mod(10);
        var checkDigit = (int)char.GetNumericValue(stringDigits[stringDigits.Length - 1]);
        var isValid = checksum == checkDigit;
        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
        }

        return errorMessage;
    }

    private static IEnumerable<string> CheckTRAlgorithm(string stringDigits)
    {
        var errorMessage = new List<string>();

        var number = stringDigits.Substring(0, stringDigits.Length - 2);
        int sum = 0;
        for (int i = 0; i < number.Length; i++)
        {
            sum = sum + (i % 2 == 0 ? 3 : 1) * (int)char.GetNumericValue(number[i]);
        }

        int check1 = (10 - sum).Mod(10);

        sum = 0;
        for (int i = 0; i < number.Length; i++)
        {
            sum = sum + (int)char.GetNumericValue(number[i]);
        }
        int check2 = (check1 + sum).Mod(10);

        var checksum = $"{check1}{check2}";
        var checkDigit = stringDigits.Substring(stringDigits.Length - 2);
        var isValid = checksum == checkDigit;
        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
        }

        return errorMessage;
    }

    private static IEnumerable<string> CheckSEAlgorithm(string stringDigits)
    {
        var errorMessage = new List<string>();

        try
        {
            var year = int.Parse(stringDigits.Substring(0, 2)) + 1900;
            var month = int.Parse(stringDigits.Substring(2, 2));
            var day = int.Parse(stringDigits.Substring(4, 2));
            DateTime date = new DateTime(year, month, day);
        }
        catch
        {
            errorMessage.Add(ValidationErrors.InvalidDate);
            return errorMessage;
        }

        var first10Digits = stringDigits.Substring(0, 10);

        return ChecksumValidationHelper.ValidateLuhnDigitNumber(first10Digits);
    }

    private static IEnumerable<string> CheckILAlgorithm(string stringDigits)
    {
        var errorMessage = new List<string>();

        int counter = 0;
        for (int i = 0; i < 9; i++)
        {
            int incNum = (int)char.GetNumericValue(stringDigits[i]);
            incNum *= (i % 2) + 1;
            if (incNum > 9)
            {
                incNum -= 9;
            }

            counter += incNum;
        }

        var checksum = counter % 10;
        var checkDigit = 0;
        var isValid = checksum == checkDigit;
        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
        }

        return errorMessage;
    }

    #region NO

    private static IEnumerable<string> CheckNOAlgorithm(string stringDigits)
    {
        var errorMessage = new List<string>();

        var isFirstCheckdigitValid = (int)char.GetNumericValue(stringDigits[9]) != CalculateChecksumNorway(stringDigits, new int[] { 3, 7, 6, 1, 8, 9, 4, 5, 2 });
        var isSecondCheckdigitValid = (int)char.GetNumericValue(stringDigits[10]) != CalculateChecksumNorway(stringDigits, new int[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 });

        if (isFirstCheckdigitValid || isSecondCheckdigitValid)
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
            return errorMessage;
        }
        else if (!HasValidDateNorway(stringDigits))
        {
            errorMessage.Add(ValidationErrors.InvalidDate);
            return errorMessage;
        }

        return errorMessage;
    }

    private static int CalculateChecksumNorway(string number, int[] weights)
    {
        int sum = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i] * (int)char.GetNumericValue(number[i]);
        }

        return 11 - sum % 11;
    }

    private static bool HasValidDateNorway(string number)
    {
        var day = int.Parse(number.Substring(0, 2));
        var month = int.Parse(number.Substring(2, 2));
        var year = int.Parse(number.Substring(4, 2));
        var individual_digits = int.Parse(number.Substring(6, 3));

        if (day >= 80)
        {
            //'This number is an FH-number, and does not contain birth date information by design.')
            return false;
        }
        if (day > 40)
        {
            day -= 40;
        }
        if (month > 40)
        {
            month -= 40;
        }

        if (individual_digits < 500)
        {
            year += 1900;
        }
        else if (500 <= individual_digits && individual_digits < 750 && year >= 54)
        {
            year += 1800;
        }
        else if (500 <= individual_digits && individual_digits < 1000 && year < 40)
        {
            year += 2000;
        }
        else if (900 <= individual_digits && individual_digits < 1000 && year >= 40)
        {
            year += 1900;
        }
        else
        {

            return false;
        }
        try
        {
            DateTime date = new DateTime(year, month, day);
            return date < DateTime.Now;
        }
        catch
        {
            return false;
        }
    }

    #endregion NO

    private static IEnumerable<string> CheckNZAlgorithm(string stringDigits)
    {
        var errorMessage = new List<string>();

        if (!(10000000 < long.Parse(stringDigits) && long.Parse(stringDigits) < 150000000))
        {
            errorMessage.Add(ValidationErrors.InvalidFormat);
            return errorMessage;
        }

        var number = stringDigits.Substring(0, stringDigits.Length - 1);

        int[] primary_weights = new int[] { 3, 2, 7, 6, 5, 4, 3, 2 };
        int[] secondary_weights = new int[] { 7, 4, 3, 2, 5, 2, 7, 6 };

        number = number.PadLeft(8, '0');
        int s = 0;
        for (int i = 0; i < number.Length; i++)
        {
            s = s + (primary_weights[i] * (int)char.GetNumericValue(number[i]));
        }

        s = (-s).Mod(11);

        if (s == 10)
        {
            s = 0;
            for (int i = 0; i < number.Length; i++)
            {
                s = s + (secondary_weights[i] * (int)char.GetNumericValue(number[i]));
            }
            s = (-s).Mod(11);
        }

        var checksum = s;
        var checkDigit = char.GetNumericValue(stringDigits[stringDigits.Length - 1]);
        var isValid = checksum == checkDigit;
        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
        }

        return errorMessage;
    }

    private static IEnumerable<string> CheckFIAlgorithm(string stringDigits)
    {
        //TODO: implement format with - or + signs instead of A
        // Currently, only 'A' format works

        var errorMessage = new List<string>();

        var day = int.Parse(stringDigits.Substring(0, 2));
        var month = int.Parse(stringDigits.Substring(2, 2));
        var year = int.Parse(stringDigits.Substring(4, 2));
        var centuries = new Dictionary<char, int>(){
            { '+',  1800 },
            { '-', 1900},
            {  'A', 2000}
            };
        try
        {
            year = centuries[stringDigits[6]] + year;
            DateTime date = new DateTime(year, month, day);
            if (date > DateTime.UtcNow)
            {
                errorMessage.Add(ValidationErrors.InvalidDate);
                return errorMessage;
            }
        }
        catch
        {
            errorMessage.Add(ValidationErrors.InvalidDate);
            return errorMessage;
        }

        var individual = int.Parse(stringDigits.Substring(7, 3));
        if (individual < 2)
        {
            errorMessage.Add(ValidationErrors.InvalidFormat);
            return errorMessage;
        }
        var n = stringDigits.Substring(0, 6) + stringDigits.Substring(7, 3);
        long intn = long.Parse(n);

        var checksum = "0123456789ABCDEFHJKLMNPRSTUVWXY"[(int)intn % 31];
        var checkDigit = stringDigits[10];
        var isValid = checksum == checkDigit;
        if (!isValid)
        {
            errorMessage.Add(ValidationErrors.ChecksumError);
        }

        return errorMessage;
    }
}