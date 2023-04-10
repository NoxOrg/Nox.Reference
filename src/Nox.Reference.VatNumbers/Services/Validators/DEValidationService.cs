﻿//using Nox.Reference.Abstractions.VatNumbers;
//using Nox.Reference.Shared;
//using Nox.Reference.VatNumbers.Constants;
//using Nox.Reference.VatNumbers.Extension;

//namespace Nox.Reference.VatNumbers.Services.Validators
//{
//    // Verifiend manually: FALSE
//    // Personal data cleaned: TRUE
//    internal class DEValidationService : VatValidationServiceBase
//    {
//        // TODO: review
//        private const string _validationPattern = @"^[1-9]\d{8}$";
//        private const string _validationPatternDescription = "VAT should have 9 numeric characters first of which is not 0.";

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

//            var product = 10;
//            for (var index = 0; index < 8; index++)
//            {
//                var sum = (int.Parse(number[index].ToString()) + product) % 10;
//                if (sum == 0)
//                {
//                    sum = 10;
//                }

//                product = 2 * sum % 11;
//            }

//            var val = 11 - product;
//            var checkDigit = val == 10
//                ? 0
//                : val;

//            var isValid = checkDigit == int.Parse(number[8].ToString());
//            if (!isValid)
//            {
//                errorMessage.Add(ValidationErrors.ChecksumError);
//            }

//            return errorMessage;
//        }
//    }
//}