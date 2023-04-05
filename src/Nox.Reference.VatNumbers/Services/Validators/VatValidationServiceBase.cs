using Nox.Reference.Abstractions.VatNumbers;
using Nox.Reference.Shared;
using Nox.Reference.VatNumbers.Constants;
using System.Text.RegularExpressions;

namespace Nox.Reference.VatNumbers.Services.Validators
{
    public abstract class VatValidationServiceBase
    {
        public abstract ValidationResult ValidateVatNumber(IVatNumberInfo vatNumber);

        public static List<string> ValidateRegex(
            string vatNumber,
            string pattern,
            string orinialVatNumber,
            string validationPatternDescription)
        {
            var errorMessages = new List<string>();

            if (!Regex.IsMatch(vatNumber, pattern))
            {
                errorMessages.Add(string.Format(ValidationErrors.WrongFormatErrorTemplate, orinialVatNumber, validationPatternDescription));
            }

            return errorMessages;
        }
    }
}
