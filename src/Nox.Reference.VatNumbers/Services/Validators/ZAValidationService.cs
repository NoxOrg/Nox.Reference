//using Nox.Reference.Abstractions.VatNumbers;
//using Nox.Reference.Shared;
//using Nox.Reference.VatNumbers.Constants;
//using Nox.Reference.VatNumbers.Extension;

//namespace Nox.Reference.VatNumbers.Services.Validators
//{
//    // Verifiend manually: FALSE
//    // Personal data cleaned: TRUE
//    internal class ZAValidationService : VatValidationServiceBase
//    {
//        public override ValidationResult ValidateVatNumber(IVatNumberInfo vatNumber)
//        {
//            // Cannot have special characters and remove optional prefix

//            if (string.IsNullOrWhiteSpace(vatNumber.FormattedVatNumber))
//            {
//                return ValidationResults.NullValidationResult;
//            }

//            var result = new ValidationResult();

//            // Code should match the pattern
//            result.ValidationErrors.AddRange(ValidateRegex(vatNumber.FormattedVatNumber, vatNumber.ValidationRegex, vatNumber.OriginalVatNumber, vatNumber.ValidationFormatDescription));

//            // Should be consisting of numbers to check checksum
//            result.ValidationErrors.AddRange(vatNumber.FormattedVatNumber.Substring(2).ValidateLuhnDigitForVatNumber());

//            return result;
//        }
//    }
//}
