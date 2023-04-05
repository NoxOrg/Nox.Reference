//using Nox.Reference.Abstractions.VatNumbers;
//using Nox.Reference.Shared;
//using Nox.Reference.VatNumbers.Constants;
//using Nox.Reference.VatNumbers.Extension;

//namespace Nox.Reference.VatNumbers.Services.Validators
//{
//    // Verifiend manually: FALSE
//    // Personal data cleaned: TRUE
//    internal class CAValidationService : VatValidationServiceBase
//    {
//        // TODO: review
//        private const string _validationPattern = @"^\d{9}$";
//        private const string _validationPatternDescription = "VAT should have 9 numeric characters";

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

//            var exactLengthRequirement = 9;
//            if (number.Length != exactLengthRequirement)
//            {
//                result.ValidationErrors.Add(string.Format(ValidationErrors.LengthShouldEqualError, exactLengthRequirement));
//            }
//            else
//            {
//                // Should be consisting of numbers to check checksum
//                result.ValidationErrors.AddRange(number.ValidateCustomChecksum(CalculateChecksum));
//            }

//            return result;
//        }

//        private static List<string> CalculateChecksum(string number)
//        {
//            var errorMessage = new List<string>();

//            int[] digits = new int[number.Length];
//            var total = digits.Where((value, index) => index % 2 == 0 && index != 8).Sum() +
//                        digits.Where((value, index) => index % 2 != 0).Select(v => v * 2).SelectMany(v => v.ToString().Select(o => Convert.ToInt32(o) - 48)).Sum();

//            var checkDigit = (10 - total % 10) % 10;

//            bool isValid = digits.Last() == checkDigit;
//            if (!isValid)
//            {
//                errorMessage.Add(ValidationErrors.ChecksumError);
//            }

//            return errorMessage;
//        }
//    }
//}
