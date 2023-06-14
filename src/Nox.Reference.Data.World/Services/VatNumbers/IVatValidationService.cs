namespace Nox.Reference;

public interface IVatValidationService
{
    /// <param name="vatNumber">Vat number as text</param>
    /// <param name="shouldValidateViaApi">Flag that indicates if API Service call needed if possible</param>
    /// <returns>Validation result</returns>
    VatNumberValidationResult ValidateVatNumber(
        string vatNumber,
        bool shouldValidateViaApi = true);

    /// <summary>
    /// Country Alpha 2 code of the validation service
    /// </summary>
    string CountryCode { get; }
}