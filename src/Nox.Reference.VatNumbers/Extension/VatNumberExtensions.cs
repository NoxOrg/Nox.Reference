using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Extension
{
    public static class VATNumberExtensions
    {
        public static string NormalizeVatNumber(this IVatNumber vatNumber)
        {
            var number = vatNumber.Number.RemoveSpecialCharacthers();

            return number.ToLower().Replace(vatNumber.CountryAlphaCode2.ToLower(), string.Empty);
        }
    }
}
