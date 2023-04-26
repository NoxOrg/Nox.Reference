using Nox.Reference.Abstractions;
using System.Reflection;

namespace Nox.Reference.Data.World;

// TODO: Issue #12: We could optionally auto-detect and validate country here in case original validation failed
public static class VatValidationService
{
    private static readonly IReadOnlyDictionary<string, IVatValidationService> _validationServicesByCountry = GetValidationServices();

    public static IVatNumberValidationResult ValidateVatNumber(
        IVatNumberDefinitionInfo vatNumberDefinition,
        string vatNumber,
        bool shouldValidateViaApi = true)
    {
        if (string.IsNullOrWhiteSpace(vatNumber))
        {
            return VatNumberValidationResult.CreateWithoutValidation(ValidationErrors.EmptyVatNumberError);
        }

        if (vatNumberDefinition == null || vatNumberDefinition.Validations?.Length < 1)
        {
            return VatNumberValidationResult.CreateWithoutValidation(ValidationErrors.ValidatorNotFoundError);
        }

        return GenericValidationService.ValidateVatNumber(vatNumber, vatNumberDefinition, shouldValidateViaApi);
    }

    public static IVatNumberValidationResult ValidateVatNumber(
         string countryCode,
         string vatNumber,
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