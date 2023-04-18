using Nox.Reference.Abstractions.VatNumbers;

namespace Nox.Reference.Holidays
{
    public interface IVatNumberService
    {
        /// <summary>
        /// Get VAT number validation info for a particular country
        /// </summary>
        /// <param name="vatNumberInfo">VAT Number object with just a country or with country and number specified</param>
        /// <returns>Vat number validation info</returns>
        IVatNumberInfo GetCountryVatInfo(IVatNumberInfo vatNumberInfo);

        /// <summary>
        /// Get VAT number validation info and validate it for a particular country
        /// </summary>
        /// <param name="vatNumberInfo">VAT Number with country and number specified</param>
        /// <param name="shouldValidateViaApi">A flag whether API validation should be used if applicable</param>
        /// <returns>Validation Vat number info</returns>
        IVatNumberInfo ValidateVatNumber(IVatNumberInfo vatNumberInfo, bool shouldValidateViaApi = true);
    }
}