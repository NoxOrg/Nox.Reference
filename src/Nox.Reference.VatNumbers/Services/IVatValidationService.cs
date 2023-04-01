using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Models;
using System.Text.RegularExpressions;

namespace Nox.Reference.VatNumbers.Services
{
    public abstract class VatValidationServiceBase
    {
        public abstract ValidationResult ValidateVatNumber(IVatNumber vatNumber);

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
