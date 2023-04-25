using System.Text;

namespace Nox.Reference.Common;

public static class StringExtensions
{
    public static string NormalizeVatNumber(this string vatNumber, string country)
    {
        var number = vatNumber.RemoveSpecialCharacthers();

        var numberWithoutCountryCode = number.ToUpper().TrimStart(country.ToCharArray());
        return $"{country.ToUpper()}{numberWithoutCountryCode}";
    }

    public static string RemoveSpecialCharacthers(this string vatNumber)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < vatNumber?.Length; i++)
        {
            if (char.IsLetterOrDigit(vatNumber[i]))
            {
                sb.Append(vatNumber[i]);
            }
        }

        return sb.ToString();
    }
}