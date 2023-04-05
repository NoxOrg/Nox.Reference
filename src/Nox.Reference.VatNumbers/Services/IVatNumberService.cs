using Nox.Reference.Abstractions.VatNumbers;

namespace Nox.Reference.Holidays
{
    public interface IVatNumberService
    {
        IVatNumberInfo GetCountryVatInfo(IVatNumberInfo vatNumberInfo);
        IVatNumberInfo ValidateVatNumber(IVatNumberInfo vatNumberInfo);
    }
}