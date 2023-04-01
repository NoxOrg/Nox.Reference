using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Extension
{
    public static class VATNumberExtensions
    {
        public static string NormalizeVatNumber(this IVatNumber vatNumber)
        {
            var number = vatNumber.Number.RemoveSpecialCharacthers();

            return number.ToUpper().TrimStart(vatNumber.CountryAlphaCode2.ToCharArray());
        }
    }
}
