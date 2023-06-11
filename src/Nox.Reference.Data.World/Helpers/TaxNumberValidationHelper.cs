using System.Text.RegularExpressions;

namespace Nox.Reference.Data.World;

public static class TaxNumberValidationHelper
{
    public static List<string> ValidateRegex(
        string taxNumber,
        string pattern,
        string originalTaxNumber,
        string validationPatternDescription)
    {
        var errorMessages = new List<string>();

        if (!Regex.IsMatch(taxNumber, pattern))
        {
            errorMessages.Add(string.Format(ValidationErrors.WrongFormatErrorTemplate, originalTaxNumber, validationPatternDescription));
        }

        return errorMessages;
    }
}