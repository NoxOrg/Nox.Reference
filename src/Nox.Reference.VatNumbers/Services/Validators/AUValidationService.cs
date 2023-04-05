//using Nox.Reference.Abstractions.VatNumbers;
//using Nox.Reference.Shared;
//using Nox.Reference.VatNumbers.Constants;
//using Nox.Reference.VatNumbers.Extension;

//namespace Nox.Reference.VatNumbers.Services.Validators
//{
//    // Verifiend manually: FALSE
//    // Personal data cleaned: TRUE
//    internal class AUValidationService : VatValidationServiceBase
//    {
//        public override ValidationResult ValidateVatNumber(IVatNumberInfo vatNumber)
//        {
//            // Cannot have special characters and remove optional prefix
//            var number = vatNumber.NormalizeVatNumber();

//            if (string.IsNullOrWhiteSpace(number))
//            {
//                return ValidationResults.NullValidationResult;
//            }

//            var result = new ValidationResult();

//            // Should be consisting of numbers to check checksum
//            result.ValidationErrors.AddRange(number.ValidateCustomChecksum(CalculateChecksum));

//            return result;
//        }

//        private static List<string> CalculateChecksum(string number)
//        {
//            var errorMessage = new List<string>();

//            var exactLengthRequirement = 11;
//            if (number.Length != exactLengthRequirement)
//            {
//                errorMessage.Add(string.Format(ValidationErrors.LengthShouldEqualError, exactLengthRequirement));
//                return errorMessage;
//            }

//            int sum = 0;
//            int[] weights = new int[] { 10, 1, 3, 5, 7, 9, 11, 13, 15, 17, 19 };
//            for (int i = 0; i < number.Length; i++)
//            {
//                if (i == 0)
//                {
//                    sum += weights[i] * ((int)char.GetNumericValue(number[i]) - 1);
//                }
//                else
//                {
//                    sum += weights[i] * (int)char.GetNumericValue(number[i]);
//                }
//            }

//            var isValid = sum % 89 == 0;
//            if (!isValid)
//            {
//                errorMessage.Add(ValidationErrors.ChecksumError);
//            }

//            return errorMessage;
//        }
//    }
//}
