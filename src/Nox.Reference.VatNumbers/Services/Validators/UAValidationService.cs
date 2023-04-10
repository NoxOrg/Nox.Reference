﻿//using Nox.Reference.Abstractions.VatNumbers;
//using Nox.Reference.Shared;
//using Nox.Reference.VatNumbers.Constants;

//namespace Nox.Reference.VatNumbers.Services.Validators
//{
//    // Verifiend manually: FALSE
//    // Personal data cleaned: TRUE
//    internal class UAValidationService : VatValidationServiceBase
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
//            if (vatNumber.Validations?.Count > 0)
//            {
//                result.ValidationErrors.AddRange(ValidateRegex(vatNumber.FormattedVatNumber, vatNumber.Validations[0].Regex, vatNumber.OriginalVatNumber, vatNumber.ValidationFormatDescription));
//            }

//            return result;
//        }
//    }
//}