namespace Nox.Reference.VatNumbers.Models
{
    public class VatNumber : IVatNumber
    {
        public VatNumber(string number, string cuntryAlphaCode2)
        {
            Number = number;
            CountryAlphaCode2 = cuntryAlphaCode2;
        }

        public string Number { get; }
        public string CountryAlphaCode2 { get; }
    }
}