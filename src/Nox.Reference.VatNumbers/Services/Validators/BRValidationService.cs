//using Nox.Reference.Abstractions.VatNumbers;
//using Nox.Reference.Shared;
//using Nox.Reference.VatNumbers.Constants;
//using Nox.Reference.VatNumbers.Extension;

//namespace Nox.Reference.VatNumbers.Services.Validators
//{
//    // Verifiend manually: FALSE
//    // Personal data cleaned: TRUE
//    internal class BRValidationService : VatValidationServiceBase
//    {
//        // TODO: review
//        private const string _validationPattern = @"^\d{14}$";
//        private const string _validationPatternDescription = "VAT should have 14 numeric characters";

//        public override ValidationResult ValidateVatNumber(IVatNumberInfo vatNumber)
//        {
//            // Cannot have special characters and remove optional prefix
//            var number = vatNumber.OriginalVatNumber.NormalizeVatNumber(vatNumber.Country);
//            number = number.Substring(2).Where(char.IsDigit).ToString();

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

//            var minimumLengthRequirement = 14;
//            if (number.Length < minimumLengthRequirement)
//            {
//                errorMessage.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLengthRequirement));
//                return errorMessage;
//            }

//            var registration = number.Substring(0, 12);
//            registration += DigitChecksum(registration);
//            registration += DigitChecksum(registration);

//            var isValid = registration.Substring(registration.Length - 2) == number.Substring(registration.Length - 2);
//            if (!isValid)
//            {
//                errorMessage.Add(ValidationErrors.ChecksumError);
//            }

//            return errorMessage;
//        }

//        private static int DigitChecksum(string numbers)
//        {
//            int index = 2;

//            char[] charArray = numbers.ToCharArray();
//            Array.Reverse(charArray);
//            numbers = new string(charArray);

//            int sum = 0;

//            for (int i = 0; i < numbers.Length; i++)
//            {
//                sum += int.Parse(numbers[i].ToString()) * index;

//                index = index == 9 ? 2 : index + 1;
//            }

//            var mod = sum % 11;

//            return mod < 2 ? 0 : 11 - mod;
//        }
//    }
//}
