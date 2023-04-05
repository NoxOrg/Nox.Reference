using Nox.Reference.Abstractions.VatNumbers;

namespace Nox.Reference.VatNumbers.Extension
{
    public static class VatNumberInfoExtensions
    {
        public static string NormalizeVatNumber(this IVatNumberInfo vatNumber)
        {
            var number = vatNumber.OriginalVatNumber.RemoveSpecialCharacthers();

            var numberWithoutCountryCode = number.ToUpper().TrimStart(vatNumber.CountryIso2Code.ToCharArray());
            return $"{vatNumber.CountryIso2Code.ToUpper()}{numberWithoutCountryCode}";
        }
    }
}
