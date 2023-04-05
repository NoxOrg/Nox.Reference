//using Nox.Reference.Abstractions.VatNumbers;
//using Nox.Reference.Shared;
//using Nox.Reference.VatNumbers.Constants;
//using Nox.Reference.VatNumbers.Extension;

//namespace Nox.Reference.VatNumbers.Services.Validators
//{
//    // Verifiend manually: FALSE
//    // Personal data cleaned: TRUE
//    internal class NLValidationService : VatValidationServiceBase
//    {
//        // TODO: review
//        private const string _validationPattern = @"^NL\d{9}B\d{2}$";
//        private const string _validationPatternDescription = "VAT should have 9 numeric characters first, then B letter and 2 numbers after it.";

//        public override ValidationResult ValidateVatNumber(IVatNumberInfo vatNumber)
//        {
//            // Cannot have special characters and remove optional prefix
//            var number = vatNumber.NormalizeVatNumber();

//            if (string.IsNullOrWhiteSpace(number))
//            {
//                return ValidationResults.NullValidationResult;
//            }

//            var result = new ValidationResult();

//            // Code should match the pattern
//            result.ValidationErrors.AddRange(ValidateRegex(number, _validationPattern, vatNumber.FormattedVatNumber, _validationPatternDescription));

//            var minimumLengthRequirement = 9;
//            if (number.Length < minimumLengthRequirement)
//            {
//                result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLengthRequirement));
//            }
//            else
//            {
//                // Should be consisting of numbers to check checksum
//                result.ValidationErrors.AddRange(number.Substring(0, 9).ValidateCustomChecksum(CalculateChecksum));
//            }

//            return result;
//        }

//        private static List<string> CalculateChecksum(string number)
//        {
//            var errorMessage = new List<string>();

//            int[] multipliers = { 9, 8, 7, 6, 5, 4, 3, 2 };
//            var sum = number.GetSumOfDigitsMulipliedByMultipliers(multipliers);

//            var checkDigit = sum % 11;

//            if (checkDigit > 9)
//            {
//                checkDigit = 0;
//            }

//            var isValid = checkDigit == int.Parse(number[8].ToString());
//            if (!isValid)
//            {
//                errorMessage.Add(ValidationErrors.ChecksumError);
//            }

//            return errorMessage;
//        }
//    }
//}
