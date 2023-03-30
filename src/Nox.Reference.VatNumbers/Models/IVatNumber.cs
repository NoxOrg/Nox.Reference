namespace Nox.Reference.VatNumbers.Models
{
    public interface IVatNumber
    {
        public string Number { get; }
        public string CountryAlphaCode2 { get; }
    }
}