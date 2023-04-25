using Nox.Reference.Abstractions;
using System.Collections.Concurrent;
using System.Reflection;

namespace Nox.Reference.Data.World;

// TODO: Issue #12: We could optionally auto-detect and validate country here in case original validation failed
public static class VatValidationService
{
    private static readonly ConcurrentDictionary<string, VatValidationServiceBase> _validationServicesByCountry = new ConcurrentDictionary<string, VatValidationServiceBase>();
    private static readonly VatValidationServiceBase _genericVatValidationServiceBase = new GenericValidationService();

    public static IVatNumberValidationResult ValidateVatNumber(
        string vatNumber,
        IVatNumberDefinitionInfo vatNumberDefinition,
        bool shouldValidateViaApi)
    {
        if (vatNumberDefinition == null)
        {
            return VatNumberValidationResult.CreateWithoutValidation(ValidationErrors.NullValueError);
        }

        var countryCode = vatNumberDefinition.Country.ToUpper();
        if (IsSupportingCountryValidation(countryCode))
        {
            return _validationServicesByCountry[countryCode].ValidateVatNumber(vatNumber, vatNumberDefinition, shouldValidateViaApi);
        }
        else if (vatNumberDefinition.Validations?.Length > 0)
        {
            return _genericVatValidationServiceBase.ValidateVatNumber(vatNumber, vatNumberDefinition, shouldValidateViaApi);
        }

        return VatNumberValidationResult.CreateWithoutValidation(ValidationErrors.ValidatorNotFoundError);
    }

    public static bool IsSupportingCountryValidation(string? iso2CountryCode)
    {
        iso2CountryCode = iso2CountryCode?.ToUpper() ?? string.Empty;

        if (_validationServicesByCountry.ContainsKey(iso2CountryCode))
        {
            return true;
        }

        var validationService = GetCountryValidationServiceType(iso2CountryCode);
        if (validationService != null)
        {
            return true;
        }

        return false;
    }

    private static VatValidationServiceBase? GetCountryValidationServiceType(string iso2CountryCode)
    {
        var validationServiceType = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .FirstOrDefault(x => x.Name.Equals($"{iso2CountryCode}ValidationService", StringComparison.OrdinalIgnoreCase));

        if (validationServiceType != null)
        {
            var instance = (VatValidationServiceBase)Activator.CreateInstance(validationServiceType)!;
            _validationServicesByCountry[iso2CountryCode] = instance;
            return instance;
        }

        return null;
    }
}