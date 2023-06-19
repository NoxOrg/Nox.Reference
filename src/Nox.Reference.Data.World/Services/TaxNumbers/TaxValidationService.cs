using Nox.Reference.Data.World;
using System.Reflection;

namespace Nox.Reference;

// TODO: Issue #12: We could optionally auto-detect and validate country here in case original validation failed
public static class TaxValidationService
{
    private static readonly IReadOnlyDictionary<string, ITaxValidationService> _validationServicesByCountry = GetValidationServices();

    /// <summary>
    /// Validates tax number string and returns validation result
    /// </summary>
    /// <param name="taxNumber">Tax number as text</param>
    /// <param name="taxNumberDefinition">Information that will be used for validation process</param>
    /// <param name="shouldValidateViaApi">Flag to determine if validation should use online API service (if applicable) or not</param>
    /// <returns>Validation result</returns>
    public static TaxNumberValidationResult ValidateTaxNumber(
        TaxNumberDefinition taxNumberDefinition,
        string taxNumber,
        bool shouldValidateViaApi = true)
    {
        if (string.IsNullOrWhiteSpace(taxNumber))
        {
            return TaxNumberValidationResult.CreateWithoutValidation(ValidationErrors.EmptyTaxNumberError);
        }

        if (taxNumberDefinition == null || taxNumberDefinition.ValidationRules?.Count() < 1)
        {
            return TaxNumberValidationResult.CreateWithoutValidation(ValidationErrors.ValidatorNotFoundError);
        }

        return GenericTaxValidationService.ValidateTaxNumber(taxNumber, taxNumberDefinition, shouldValidateViaApi);
    }

    /// <summary>
    /// Validates tax number string and returns validation result
    /// </summary>
    /// <param name="taxNumber">Tax number as text</param>
    /// <param name="countryCode">Tax number country Alpha 2 code</param>
    /// <param name="shouldValidateViaApi">Flag to determine if validation should use online API service (if applicable) or not</param>
    /// <returns>Validation result</returns>
    public static TaxNumberValidationResult ValidateVatNumber(
         string taxNumber,
         string countryCode,
         bool shouldValidateViaApi = true)
    {
        if (string.IsNullOrWhiteSpace(taxNumber))
        {
            return TaxNumberValidationResult.CreateWithoutValidation(ValidationErrors.EmptyVatNumberError);
        }
        if (string.IsNullOrWhiteSpace(countryCode))
        {
            return TaxNumberValidationResult.CreateWithoutValidation(ValidationErrors.EmptyCountryError);
        }

        var iso2CountryCode = countryCode?.ToUpper() ?? string.Empty;
        var isSupportCountry = _validationServicesByCountry.ContainsKey(iso2CountryCode);

        if (isSupportCountry)
        {
            var validationService = _validationServicesByCountry[iso2CountryCode];
            return validationService.ValidateTaxNumber(taxNumber, shouldValidateViaApi);
        }

        return TaxNumberValidationResult.CreateWithoutValidation(ValidationErrors.ValidatorNotFoundError);
    }

    /// <summary>
    /// Returns validation service to country map
    /// </summary>
    /// <returns>Validation service per country map</returns>
    public static IReadOnlyDictionary<string, ITaxValidationService> GetValidationServices()
    {
        var validationServiceTypes = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(x => typeof(ITaxValidationService).IsAssignableFrom(x))
            .ToArray();

        var validationServices = new Dictionary<string, ITaxValidationService>();
        foreach (var validationServiceType in validationServiceTypes)
        {
            var instance = (ITaxValidationService)Activator.CreateInstance(validationServiceType)!;
            validationServices[instance.CountryCode] = instance;
        }

        return validationServices;
    }
}