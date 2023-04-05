//using Nox.Reference.Abstractions.VatNumbers;
//using Nox.Reference.Shared;
//using Nox.Reference.VatNumbers.Constants;
//using Nox.Reference.VatNumbers.Extension;

//namespace Nox.Reference.VatNumbers.Services.Validators
//{
//    // Verifiend manually: FALSE
//    // Personal data cleaned: TRUE
//    internal class FRValidationService : VatValidationServiceBase
//    {
//        // TODO: this alghorym is very weird, need to review it and possibly rewrite from scratch
//        private const string _validationPattern = @"^\d{11}$";
//        private const string _validationPatternDescription = "VAT should have 11 numeric characters and optional 'FR' text in upper or lower case front of them.";

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

//            var exactLengthRequirement = 11;
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

//            var checksumDigits = int.Parse(number.Substring(0, 2));
//            var checksum = long.Parse(number.Substring(2) + "12") % 97;

//            var isValid = checksum == checksumDigits;
//            if (!isValid)
//            {
//                errorMessage.Add(ValidationErrors.ChecksumError);
//            }

//            return errorMessage;
//        }
//    }
//}
