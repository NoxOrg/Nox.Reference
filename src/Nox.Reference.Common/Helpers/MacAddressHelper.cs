using System.Text.RegularExpressions;

namespace Nox.Reference.Common.Helpers;

internal static class MacAddressHelper
{
    private const string MacAddressRegex = "^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$";

    public static string GetMacAddressPrefix(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException(input);
        }

        var isMatch = Regex.Match(input, MacAddressRegex, RegexOptions.IgnoreCase).Success;

        var sanitizedInput = input
            .Replace("-", "")
            .Replace(":", "")
            [..6];

        if (!isMatch)
        {
            // TODO: Provide particular type of exception
            throw new ArgumentException("Provided input does not satisfy Mac Address definition");
        }

        return sanitizedInput;
    }
}