using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Models;
using System.Text.RegularExpressions;

namespace Nox.Reference.VatNumbers.Services
{
    internal interface IVatValidationService
    {
        public ValidationResult ValidateVatNumber(IVatNumber vatNumber);

        public static void ValidateRegex(
            ValidationResult result,
            string vatNumber,
            string pattern,
            string orinialVatNumber,
            string validationPatternDescription)
        {
            if (!Regex.IsMatch(vatNumber, pattern))
            {
                result.ValidationErrors.Add(string.Format(ValidationErrors.WrongFormatErrorTemplate, orinialVatNumber, validationPatternDescription));
            }
        }
    }
}
