using Nox.Reference.VatNumbers.Constants;

namespace Nox.Reference.VatNumbers.Extension
{
    internal static class StringChecksumValidationExtensions
    {
        public static List<string> ValidateCustomChecksum(this string vatNumber, Func<string, List<string>> checksumFunc)
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

        public static List<string> ValidateLuhnDigitForVatNumber(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckLuhnDigit());
        }

        public static List<string> ValidateModAndSubstract(this string vatNumber, int modulus, List<int> weights, int checksumDigitPosition)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckModAndSubstract(modulus, weights, checksumDigitPosition));
        }

        public static List<string> ValidateModAndSubstractItaly(this string vatNumber, int modulus, List<int> weights)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckModAndSubstractItaly(modulus, weights));
        }

        public static List<string> ValidateMod(this string vatNumber, int modulus, List<int> weights, int checksumDigitPosition)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckMod(modulus, weights, checksumDigitPosition));
        }

        public static List<string> ValidateMexicanAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckMexicanAlgorithm());
        }

        public static List<string> ValidateGermanAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckGermanAlgorithm());
        }

        public static List<string> ValidateFrenchAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckFrenchAlgorithm());
        }

        public static List<string> ValidateColombianAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckColombianAlgorithm());
        }

        public static List<string> ValidateAustralianAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckAustralianAlgorithm());
        }

        public static List<string> ValidateBelgianAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckBelgianAlgorithm());
        }

        public static List<string> ValidateBralizianAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckBrazilianAlgorithm());
        }

        public static List<string> ValidateCanadianAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckCanadianAlgorithm());
        }

        public static List<string> ValidateSwissAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckSwissAlgorithm());
        }

        public static List<string> ValidateBritishAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckBritishAlgorithm());
        }

        public static List<string> ValidateSpanish1Algorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckSpanish1Algorithm());
        }

        public static List<string> ValidateSpanish2Algorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckSpanish2Algorithm());
        }

        public static List<string> ValidateSpanish3Algorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckSpanish3Algorithm());
        }

        public static List<string> ValidateDenmarkAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckDenmarkAlgorithm());
        }

        public static List<string> ValidateAustrianAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckAustrianAlgorithm());
        }

        public static List<string> ValidateJapaneseAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckJapeneseAlgorithm());
        }

        public static List<string> ValidateChineseAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckChineseAlgorithm());
        }

        public static List<string> ValidateTurkishAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckTurkishAlgorithm());
        }

        public static List<string> ValidateSwedenAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckSwedenAlgorithm());
        }

        public static List<string> ValidateIsraeliAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckIsraeliAlgorithm());
        }

        public static List<string> ValidateNorwegianAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckNorwegianAlgorithm());
        }

        public static List<string> ValidateRussianAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckRussianAlgorithm());
        }

        public static List<string> ValidateNewZealandAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckNewZealandAlgorithm());
        }

        public static List<string> ValidateIndonesianAlgorithm(this string vatNumber)
        {
            return vatNumber.ValidateCustomChecksum((vatNumber) => vatNumber.CheckIndonesianAlgorithm());
        }

        private static List<string> CheckLuhnDigit(this string stringDigits)
        {
            var errorMessage = new List<string>();

            var lastDigit = (int)char.GetNumericValue(stringDigits[stringDigits.Length - 1]);
            var vatNumber = stringDigits.Substring(0, stringDigits.Length - 1);
            var digits = vatNumber.Select(c => (int)char.GetNumericValue(c)).ToList();
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

        private static List<string> CheckModAndSubstract(this string stringDigits, int modulus, List<int> weights, int checksumDigitPosition)
        {
            var errorMessage = new List<string>();

            var sum = 0;
            if (checksumDigitPosition < 0)
            {
                checksumDigitPosition = stringDigits.Length - Math.Abs(checksumDigitPosition);
            }

            // TODO: think about proper handling cases of different length
            // TODO: possibly change way to operate with checksum digit position
            for (var index = 0; index < weights.Count && index < checksumDigitPosition; index++)
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

        private static List<string> CheckMod(this string stringDigits, int modulus, List<int> weights, int checksumDigitPosition)
        {
            var errorMessage = new List<string>();

            var sum = 0;
            if (checksumDigitPosition < 0)
            {
                checksumDigitPosition = stringDigits.Length - Math.Abs(checksumDigitPosition);
            }

            // TODO: think about proper handling cases of different length
            // TODO: possibly change way to operate with checksum digit position
            for (var index = 0; index < weights.Count && index < checksumDigitPosition; index++)
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

        private static List<string> CheckModAndSubstractItaly(this string stringDigits, int modulus, List<int> weights)
        {
            var errorMessage = new List<string>();

            var temp = int.Parse(stringDigits.Substring(7, 3));

            if ((temp < 1 || temp > 201) && temp != 999 && temp != 888)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
                return errorMessage;
            }

            var sum = 0;

            // TODO: think about proper handling cases of different length
            // TODO: possibly change way to operate with checksum digit position
            for (var index = 0; index < weights.Count; index++)
            {
                temp = int.Parse(stringDigits[index].ToString()) * weights[index];
                sum += temp > 9
                    ? (int)Math.Floor(temp / 10D) + temp % 10
                    : temp;
            }

            var checkDigit = int.Parse(stringDigits.Last().ToString());
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

        private static List<string> CheckMexicanAlgorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();
            var originalCheckDigit = stringDigits.Last();
            var vatNumber = stringDigits.Substring(0, stringDigits.Length - 1);

            var minimumLength = 9;
            if (vatNumber.Length < minimumLength)
            {
                errorMessage.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                return errorMessage;
            }

            if (!HasValidDate(vatNumber.Substring(3, 6)))
            {
                errorMessage.Add(ValidationErrors.MX_InvalidDate);
            }

            string alphabet = "0123456789ABCDEFGHIJKLMN&OPQRSTUVWXYZ Ñ";
            vatNumber = "   " + vatNumber;
            // get last 12 characters
            vatNumber = vatNumber.Substring(vatNumber.Length - 12);

            int sum = 0;
            for (int i = 0; i < vatNumber.Length; i++)
            {
                sum += alphabet.IndexOf(vatNumber[i]) * (13 - i);
            }

            var checkDigit = alphabet[(11 - sum).Mod(11)];

            bool isValid = originalCheckDigit == checkDigit;
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static bool HasValidDate(string number)
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

        private static List<string> CheckGermanAlgorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();

            var product = 10;
            for (var index = 0; index < 8; index++)
            {
                var sum = (int.Parse(stringDigits[index].ToString()) + product) % 10;
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

            var isValid = checkDigit == int.Parse(stringDigits.Last().ToString());
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static List<string> CheckFrenchAlgorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();

            var checksumDigits = int.Parse(stringDigits.Substring(0, 2));
            var checksum = decimal.Parse(stringDigits.Substring(2) + "12") % 97;

            var isValid = checksum == checksumDigits;
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static List<string> CheckColombianAlgorithm(this string stringDigits)
        {
            var weights = new int[] { 3, 7, 13, 17, 19, 23, 29, 37, 41, 43, 47, 53, 59, 67, 71 };
            
            var lastDigit = stringDigits[stringDigits.Length - 1];
            var vatNumber = stringDigits.Substring(0, stringDigits.Length - 1);

            var errorMessage = new List<string>();

            var s = 0;

            var charArray = vatNumber.ToCharArray();
            Array.Reverse(charArray);
            vatNumber = new string(charArray);

            for (var i = 0; i < vatNumber.Length; i++)
            {
                s += weights[i] * (int)char.GetNumericValue(vatNumber[i]);
            }

            s %= 11;

            var checkDigit = "01987654321"[s];
            var isValid = checkDigit == lastDigit;
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static List<string> CheckAustralianAlgorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();

            int sum = 0;
            int[] weights = new int[] { 10, 1, 3, 5, 7, 9, 11, 13, 15, 17, 19 };
            for (int i = 0; i < weights.Length && i < stringDigits.Length; i++)
            {
                if (i == 0)
                {
                    sum += weights[i] * ((int)char.GetNumericValue(stringDigits[i]) - 1);
                }
                else
                {
                    sum += weights[i] * (int)char.GetNumericValue(stringDigits[i]);
                }
            }

            var isValid = sum % 89 == 0;
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static List<string> CheckBelgianAlgorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();

            var vatNumber = stringDigits;
            if (stringDigits.Length == 9)
            {
                vatNumber = vatNumber.PadLeft(10, '0');
            }

            var checkPart = int.Parse(vatNumber.Substring(0, 8));
            var checkDigits = int.Parse(vatNumber.Substring(8, 2));
            var isValid = 97 - checkPart % 97 == checkDigits;
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static List<string> CheckBrazilianAlgorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();

            var registration = stringDigits.Substring(0, 12);
            registration += DigitChecksum(registration);
            registration += DigitChecksum(registration);

            var checkPart = registration.Substring(registration.Length - 2);
            var checkDigits = stringDigits.Substring(registration.Length - 2);
            var isValid = checkPart == checkDigits;
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static int DigitChecksum(string numbers)
        {
            int index = 2;

            char[] charArray = numbers.ToCharArray();
            Array.Reverse(charArray);
            numbers = new string(charArray);

            decimal sum = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                sum += decimal.Parse(numbers[i].ToString()) * index;

                index = index == 9 ? 2 : index + 1;
            }

            var mod = sum % 11;

            return Convert.ToInt32(mod < 2 ? 0 : 11 - mod);
        }

        private static List<string> CheckCanadianAlgorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();

            int[] digits = new int[stringDigits.Length];
            var total = digits.Where((value, index) => index % 2 == 0 && index != 8).Sum() +
                        digits.Where((value, index) => index % 2 != 0).Select(v => v * 2).SelectMany(v => v.ToString().Select(o => Convert.ToInt32(o) - 48)).Sum();

            var checkDigit = (10 - total % 10) % 10;

            bool isValid = digits.Last() == checkDigit;
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static List<string> CheckSwissAlgorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();

            var sum = 0;
            int[] weight = new int[] { 5, 4, 3, 2, 7, 6, 5, 4 };
            for (var i = 0; i < 8; i++)
            {
                sum += (int)char.GetNumericValue(stringDigits[i]) * weight[i];
            }

            sum = 11 - sum % 11;
            if (sum == 10)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
                return errorMessage;
            }

            if (sum == 11)
            {
                sum = 0;
            }

            var checkSum = sum.ToString();
            var checkDigit = stringDigits.Substring(8, 1);
            var isValid = checkSum == checkDigit;
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static List<string> CheckBritishAlgorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();

            var total = 0;
            if (stringDigits[0] == '0')
            {
                errorMessage.Add(string.Format(ValidationErrors.WrongFormatErrorTemplate, stringDigits, "First character cannot be 0"));
            }

            if (errorMessage.Count > 0)
            {
                return errorMessage;
            }

            var multipliers = new int[] { 8, 7, 6, 5, 4, 3, 2 };

            var no = long.Parse(stringDigits.Substring(0, 7));

            for (int i = 0; i < 7; i++)
            {
                total += int.Parse(stringDigits[i].ToString()) * multipliers[i];
            }

            int cd = total;
            while (cd > 0)
            {
                cd -= 97;
            }

            cd = Math.Abs(cd);
            if (cd == int.Parse(stringDigits.Substring(7, 2)) &&
                no < 9990001 &&
                (no < 100000 || no > 999999) &&
                (no < 9490001 || no > 9700000))
            {
                return errorMessage;
            }

            cd = cd >= 55 ? cd - 55 : cd + 42;

            bool isValid = cd == int.Parse(stringDigits.Substring(7, 2)) && no > 1000000;
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static List<string> CheckSpanish1Algorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();
            var total = 0;
            var multipliers = new int[] { 2, 1, 2, 1, 2, 1, 2 };

            int temp;
            for (int i = 0; i < multipliers.Length; i++)
            {
                temp = int.Parse(stringDigits[i].ToString()) * multipliers[i];
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

            bool isValid = totalChar == stringDigits.Last();
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static List<string> CheckSpanish2Algorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();
            var total = 0;
            var multipliers = new int[] { 2, 1, 2, 1, 2, 1, 2 };

            int temp;
            for (int i = 0; i < multipliers.Length; i++)
            {
                temp = int.Parse(stringDigits[i].ToString()) * multipliers[i];
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

            bool isValid = total.ToString() == stringDigits.Last().ToString();
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static List<string> CheckSpanish3Algorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();

            var vatNumber = stringDigits;
            if (vatNumber[0] == 'Y')
            {
                vatNumber = vatNumber.Replace("Y", "1");
            }
            else if (vatNumber[0] == 'Z')
            {
                vatNumber = vatNumber.Replace("Z", "2");
            }
            else if (!char.IsDigit(vatNumber[0]))
            {
                vatNumber = vatNumber.Substring(1);
            }

            bool isValid = vatNumber.Last() == "TRWAGMYFPDXBNJZSQVHLCKE"[int.Parse(vatNumber.Substring(0, vatNumber.Length - 1)) % 23];
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        // TODO: join with MOD
        private static List<string> CheckDenmarkAlgorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();

            var multipliers = new int[] { 2, 7, 6, 5, 4, 3, 2, 1 };
            var sum = 0;

            // TODO: think about proper handling cases of different length
            // TODO: possibly change way to operate with checksum digit position
            for (var index = 0; index < multipliers.Length; index++)
            {
                if (stringDigits.Length <= index)
                {
                    errorMessage.Add(ValidationErrors.ChecksumError);
                    break;
                }

                var digit = int.Parse(stringDigits[index].ToString());
                sum += int.Parse(multipliers[index].ToString()) * digit;
            }
            var checksum = sum % 11;
            bool isValid = checksum == 0;
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        // TODO: join with other, possibly with Luhn
        private static List<string> CheckAustrianAlgorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();

            var sum = 0;
            int[] multipliers = { 1, 2, 1, 2, 1, 2, 1 };

            for (var i = 0; i < multipliers.Length; i++)
            {
                var temp = int.Parse(stringDigits[i].ToString()) * multipliers[i];
                sum += temp > 9 ? (int)Math.Floor(temp / 10D) + temp % 10 : temp;
            }

            var checksum = 10 - (sum + 4) % 10;
            if (checksum == 10)
            {
                checksum = 0;
            }

            var checkDigit = int.Parse(stringDigits[7].ToString());
            bool isValid = checksum == checkDigit;
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static List<string> CheckJapeneseAlgorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();

            // TODO: implement

            return errorMessage;
        }

        // TODO: join with ModAndSubstract
        private static List<string> CheckChineseAlgorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();

            var tempSum = 0;
            var weights = new int[]  { 1, 3, 9, 27, 19, 26, 16, 17, 20, 29, 25, 13, 8, 24, 10, 30, 28 };

            for (var i = 0; i < weights.Length; i++)
            {
                var t = int.Parse(stringDigits[i].ToString());
                tempSum += weights[i] * t;
            }
            var checksum = 31 - tempSum % 31;
            if (checksum == 31)
                checksum = 0;

            var isValid = checksum.ToString() == stringDigits.Last().ToString();
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static List<string> CheckTurkishAlgorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();

            int s = 0;
            char[] charArray = stringDigits.Substring(0, stringDigits.Length - 1).ToCharArray();
            Array.Reverse(charArray);
            var vatToCheck = new string(charArray);
            for (int i = 1; i <= vatToCheck.Length; i++)
            {
                int c1 = ((int)char.GetNumericValue(vatToCheck[i - 1]) + i) % 10;
                if (c1 != 0)
                {
                    int c2 = (c1 * (int)Math.Pow(2, i)) % 9;
                    if (c2 == 0)
                    {
                        c2 = 9;
                    }
                    s += c2;
                }
            }

            var checksum = (10 - s).Mod(10);
            var isValid = checksum.ToString() == stringDigits.Last().ToString();
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static List<string> CheckSwedenAlgorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();

            int[] Multipliers = { 2, 1, 2, 1, 2, 1, 2, 1, 2 };

            var index = 0;
            var sum = 0;
            foreach (var m in Multipliers)
            {
                var temp = int.Parse(stringDigits[index++].ToString()) * m;
                sum += temp > 9 ? (int)Math.Floor(temp / 10D) + temp % 10 : temp;
            }

            var checkDigit = 10 - sum % 10;

            if (checkDigit == 10)
            {
                checkDigit = 0;
            }

            var isValid = checkDigit.ToString() == stringDigits.Last().ToString();
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static List<string> CheckIsraeliAlgorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();

            int[] Multipliers = { 2, 1, 2, 1, 2, 1, 2, 1, 2 };

            var index = 0;
            var sum = 0;
            foreach (var m in Multipliers)
            {
                var temp = int.Parse(stringDigits[index++].ToString()) * m;
                sum += temp > 9 ? (int)Math.Floor(temp / 10D) + temp % 10 : temp;
            }

            var checkDigit = 10 - sum % 10;

            if (checkDigit == 10)
            {
                checkDigit = 0;
            }

            var isValid = checkDigit.ToString() == stringDigits.Last().ToString();
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static List<string> CheckNorwegianAlgorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();

            var total = 0;
            int[] multipliers = new int[] { 3, 2, 7, 6, 5, 4, 3, 2 };

            for (var i = 0; i < multipliers.Length; i++)
            {
                total += int.Parse(stringDigits[i].ToString()) * multipliers[i];
            }

            total = 11 - total % 11;
            if (total == 11)
            {
                total = 0;
            }

            if (total > 10)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
                return errorMessage;
            }

            var isValid = total.ToString() == stringDigits.Last().ToString();
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static List<string> CheckRussianAlgorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();

            var total = 0;
            int[] multipliers;
            bool isValid;

            if (stringDigits.Length == 10)
            {
                multipliers = new int[] { 2, 4, 10, 3, 5, 9, 4, 6, 8 };

                for (var i = 0; i < multipliers.Length; i++)
                {
                    total += int.Parse(stringDigits[i].ToString()) * multipliers[i];
                }

                total = total % 11 % 10;
                isValid = total.ToString() == stringDigits.Last().ToString();
                if (!isValid)
                {
                    errorMessage.Add(ValidationErrors.ChecksumError);
                }

                return errorMessage;
            }

            if (stringDigits.Length != 12)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
                return errorMessage;
            }

            multipliers = new int[] { 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };
            var secondDigitMultipliers = new int[] { 3, 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };
            var total2 = 0;

            for (var i = 0; i < multipliers.Length; i++)
            {
                total += int.Parse(stringDigits[i].ToString()) * multipliers[i];
                total2 += int.Parse(stringDigits[i].ToString()) * secondDigitMultipliers[i];
            }

            total2 += int.Parse(stringDigits[10].ToString()) * secondDigitMultipliers[10];

            var calculatedDigit10 = total % 11 % 10;
            var calculatedDigit11 = total2 % 11 % 10;

            int checkDigit10 = int.Parse(stringDigits[10].ToString());
            int checkDigit11 = int.Parse(stringDigits[11].ToString());
            isValid = checkDigit10 == calculatedDigit10 && checkDigit11 == calculatedDigit11;
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static List<string> CheckNewZealandAlgorithm(this string stringDigits)
        {
            var errorMessage = new List<string>();

            var numericVat = long.Parse(stringDigits);
            if (!(10000000 < numericVat && numericVat < 150000000))
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
                return errorMessage;
            }

            int[] primary_weights = new int[] { 3, 2, 7, 6, 5, 4, 3, 2 };
            int[] secondary_weights = new int[] { 7, 4, 3, 2, 5, 2, 7, 6 };

            var number = stringDigits.Substring(0, stringDigits.Length - 1).PadLeft(8, '0');
            int s = 0;
            for (int i = 0; i < primary_weights.Length; i++)
            {
                s = s + (primary_weights[i] * (int)char.GetNumericValue(number[i]));
            }
            s = (-s).Mod(11);


            int checkDigit;
            if (s != 10)
            {
                checkDigit = s;
            }
            else
            {
                s = 0;
                for (int i = 0; i < secondary_weights.Length; i++)
                {
                    s = s + (secondary_weights[i] * (int)char.GetNumericValue(number[i]));
                }

                checkDigit = (-s).Mod(11);
            }

            var isValid = checkDigit.ToString() == stringDigits.Last().ToString();
            if (!isValid)
            {
                errorMessage.Add(ValidationErrors.ChecksumError);
            }

            return errorMessage;
        }

        private static List<string> CheckIndonesianAlgorithm(this string stringDigits)
        {
            var vatNumber = stringDigits;
            if (vatNumber.Length == 12)
            {
                vatNumber += "000";
            }

            return vatNumber.Substring(0, 10).CheckLuhnDigit();
        }
    }
}
