//using Nox.Reference.Abstractions.VatNumbers;
//using Nox.Reference.Shared;
//using Nox.Reference.VatNumbers.Constants;
//using Nox.Reference.VatNumbers.Extension;

//namespace Nox.Reference.VatNumbers.Services.Validators
//{
//    // Verifiend manually: FALSE
//    // Personal data cleaned: TRUE
//    internal class ITValidationService : VatValidationServiceBase
//    {
//        private const string _validationPattern = @"^\d{11}$";
//        private const string _validationPatternDescription = "VAT should consist of 11 numeric characters.";

//        public override ValidationResult ValidateVatNumber(IVatNumberInfo vatNumber)
//        {
//            // Cannot have special characters and remove optional prefix
//            var number = vatNumber.OriginalVatNumber.NormalizeVatNumber(vatNumber.Country);
//            number = number.Substring(2);

//            if (string.IsNullOrWhiteSpace(number))
//            {
//                return ValidationResults.NullValidationResult;
//            }

//            var result = new ValidationResult();

//            // Code should match the pattern
//            result.ValidationErrors.AddRange(ValidateRegex(number, _validationPattern, vatNumber.FormattedVatNumber, _validationPatternDescription));

//            // Should be consisting of numbers to check checksum
//            result.ValidationErrors.AddRange(number.ValidateCustomChecksum(CalculateChecksum));

//            return result;
//        }

//        private static List<string> CalculateChecksum(string number)
//        {
//            var errorMessage = new List<string>();

//            var minimumLengthRequirement = 11;
//            if (number.Length != minimumLengthRequirement)
//            {
//                errorMessage.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLengthRequirement));
//                return errorMessage;
//            }

//            int[] Multipliers = { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };

//            var res = long.Parse(number);
//            if (res == 0)
//            {
//                errorMessage.Add(string.Format(ValidationErrors.WrongFormatErrorTemplate, number, "Cannot have 7 zeroes as first 7 digits."));
//                return errorMessage;
//            }

//            var temp = int.Parse(number.Substring(7, 3));

//            if ((temp < 1 || temp > 201) && temp != 999 && temp != 888)
//            {
//                errorMessage.Add(ValidationErrors.ChecksumError);
//                return errorMessage;
//            }

//            var index = 0;
//            var sum = 0;
//            foreach (var m in Multipliers)
//            {
//                temp = int.Parse(number[index++].ToString()) * m;
//                sum += temp > 9
//                    ? (int)Math.Floor(temp / 10D) + temp % 10
//                    : temp;
//            }

//            var checkDigit = 10 - sum % 10;

//            if (checkDigit > 9)
//            {
//                checkDigit = 0;
//            }

//            var isValid = checkDigit == int.Parse(number[10].ToString());
//            if (!isValid)
//            {
//                errorMessage.Add(ValidationErrors.ChecksumError);
//            }

//            return errorMessage;
//        }
//    }
//}
