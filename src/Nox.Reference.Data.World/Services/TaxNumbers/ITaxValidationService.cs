namespace Nox.Reference;

public interface ITaxValidationService
{
    /// <param name="taxNumber">Tax number as text</param>
    /// <param name="shouldValidateViaApi">Flag that indicates if API Service call needed if possible</param>
    /// <returns>Validation result</returns>
    TaxNumberValidationResult ValidateTaxNumber(
        string taxNumber,
        bool shouldValidateViaApi = true);

    /// <summary>
    /// Country Alpha 2 code of the validation service
    /// </summary>
    string CountryCode { get; }
}