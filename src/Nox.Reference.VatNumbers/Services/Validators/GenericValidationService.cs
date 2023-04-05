using Nox.Reference.Abstractions.VatNumbers;
using Nox.Reference.Shared;
using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;

namespace Nox.Reference.VatNumbers.Services.Validators
{
    public class GenericValidationService : VatValidationServiceBase
    {
        public override ValidationResult ValidateVatNumber(IVatNumberInfo vatNumber)
        {
            //if (vatNumber.IsUsingNullCheck)
            //{
            //    // Cannot have special characters and remove optional prefix
            //    if (string.IsNullOrWhiteSpace(vatNumber.FormattedVatNumber))
            //    {
            //        return ValidationResults.NullValidationResult;
            //    }
            //}

            var result = new ValidationResult();

            //if (vatNumber.IsUsingRegexValidation)
            //{
            //    // Code should match the pattern
            //    result.ValidationErrors.AddRange(ValidateRegex(vatNumber.FormattedVatNumber, vatNumber.ValidationRegex, vatNumber.OriginalVatNumber, vatNumber.ValidationFormatDescription));
            //}

            //if (vatNumber.IsUsingChecksumValidation)
            //{
            //    switch (vatNumber.ChecksumAlgorithm)
            //    {
            //        case "Luhn":
            //            result.ValidationErrors.AddRange(vatNumber.FormattedVatNumber.Substring(2).ValidateLuhnDigitForVatNumber());
            //            break;
            //        default:
            //            result.ValidationErrors.Add(ValidationErrors.UnknownChecksumAlgorithm);
            //            break;
            //    }
            //}

            return result;
        }
    }
}
