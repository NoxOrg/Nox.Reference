using System.Reflection;
using Nox.Reference.Data.World.Models;

namespace Nox.Reference.Data.World.Services.VatNumbers;

// TODO: Issue #12: We could optionally auto-detect and validate country here in case original validation failed
public static class VatValidationService
{
    private static readonly IReadOnlyDictionary<string, IVatValidationService> _validationServicesByCountry = GetValidationServices();

    /// <summary>
    /// Validates vat number string and returns validation result
    /// </summary>
    /// <param name="vatNumber">Vat number as text</param>
    /// <param name="vatNumberDefinition">Information that will be used for validation process</param>
    /// <param name="shouldValidateViaApi">Flag to determine if validation should use online API service (if applicable) or not</param>
    /// <returns>Validation result</returns>
    public static VatNumberValidationResult ValidateVatNumber(
        VatNumberDefinition vatNumberDefinition,
        string vatNumber,
        bool shouldValidateViaApi = true)
    {
        if (string.IsNullOrWhiteSpace(vatNumber))
        {
            return VatNumberValidationResult.CreateWithoutValidation(ValidationErrors.EmptyVatNumberError);
        }

        if (vatNumberDefinition == null || vatNumberDefinition.ValidationRules?.Count() < 1)
        {
            return VatNumberValidationResult.CreateWithoutValidation(ValidationErrors.ValidatorNotFoundError);
        }

        return GenericVatValidationService.ValidateVatNumber(vatNumber, vatNumberDefinition, shouldValidateViaApi);
    }

    /// <summary>
    /// Validates vat number string and returns validation result
    /// </summary>
    /// <param name="vatNumber">Vat number as text</param>
    /// <param name="countryCode">VAT number country Alpha 2 code</param>
    /// <param name="shouldValidateViaApi">Flag to determine if validation should use online API service (if applicable) or not</param>
    /// <returns>Validation result</returns>
    public static VatNumberValidationResult ValidateVatNumber(
         string vatNumber,
         string countryCode,
         bool shouldValidateViaApi = true)
    {
        if (string.IsNullOrWhiteSpace(vatNumber))
        {
            return VatNumberValidationResult.CreateWithoutValidation(ValidationErrors.EmptyVatNumberError);
        }
        if (string.IsNullOrWhiteSpace(countryCode))
        {
            return VatNumberValidationResult.CreateWithoutValidation(ValidationErrors.EmptyCountryError);
        }

        var iso2CountryCode = countryCode?.ToUpper() ?? string.Empty;
        var isSupportCountry = _validationServicesByCountry.ContainsKey(iso2CountryCode);

        if (isSupportCountry)
        {
            var validationService = _validationServicesByCountry[iso2CountryCode];
            return validationService.ValidateVatNumber(vatNumber, shouldValidateViaApi);
        }

        return VatNumberValidationResult.CreateWithoutValidation(ValidationErrors.ValidatorNotFoundError);
    }

    /// <summary>
    /// Returns validation service to country map
    /// </summary>
    /// <returns>Validation service per country map</returns>
    public static IReadOnlyDictionary<string, IVatValidationService> GetValidationServices()
    {
        var validationServiceTypes = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(x => typeof(IVatValidationService).IsAssignableFrom(x))
            .ToArray();

        var validationServices = new Dictionary<string, IVatValidationService>();
        foreach (var validationServiceType in validationServiceTypes)
        {
            var instance = (IVatValidationService)Activator.CreateInstance(validationServiceType)!;
            validationServices[instance.CountryCode] = instance;
        }

        return validationServices;
    }
}