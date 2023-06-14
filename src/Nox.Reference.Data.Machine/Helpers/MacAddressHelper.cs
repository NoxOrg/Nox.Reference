using System.Text.RegularExpressions;

namespace Nox.Reference.Data.Machine;

internal static class MacAddressHelper
{
    private static Regex MacAddressRegex = new Regex(@"^([0-9A-Fa-f]{2}[-: ]?){5}([0-9A-Fa-f]{2})$", RegexOptions.IgnoreCase);

    public static string GetMacAddressPrefix(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException(input);
        }

        var isMatch = MacAddressRegex.Match(input).Success;

        var sanitizedInput = input
            .Replace("-", "")
            .Replace(":", "")
            .Replace(" ", "")
            [..6];

        if (!isMatch)
        {
            // TODO: Provide particular type of exception
            throw new ArgumentException("Provided input does not satisfy Mac Address definition");
        }

        return sanitizedInput;
    }
}