using System.Text.RegularExpressions;

namespace Nox.Reference;

public static class VatNumberValidationHelper
{
    public static List<string> ValidateRegex(
        string vatNumber,
        string pattern,
        string originalVatNumber,
        string validationPatternDescription)
    {
        var errorMessages = new List<string>();

        if (!Regex.IsMatch(vatNumber, pattern))
        {
            errorMessages.Add(string.Format(ValidationErrors.WrongFormatErrorTemplate, originalVatNumber, validationPatternDescription));
        }

        return errorMessages;
    }
}