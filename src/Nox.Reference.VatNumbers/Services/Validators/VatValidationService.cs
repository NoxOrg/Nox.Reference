﻿using Nox.Reference.Abstractions.VatNumbers;
using Nox.Reference.Shared;
using Nox.Reference.VatNumbers.Constants;
using System.Collections.Concurrent;
using System.Reflection;

namespace Nox.Reference.VatNumbers.Services.Validators
{
    // TODO: Issue #12: We could optionally auto-detect and validate country here in case original validation failed
    public class VatValidationService
    {
        private static readonly ConcurrentDictionary<string, VatValidationServiceBase> _validationServicesByCountry = new ConcurrentDictionary<string, VatValidationServiceBase>();
        private static readonly VatValidationServiceBase _genericVatValidationServiceBase = new GenericValidationService();

        public static ValidationResult ValidateVatNumber(IVatNumberInfo vatNumber)
        {
            if (vatNumber == null)
            {
                return ValidationResults.NullValidationResult;
            }

            var result = ValidateVatNumberInternal(vatNumber);

            return result;
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

        private static ValidationResult ValidateVatNumberInternal(IVatNumberInfo vatNumber)
        {
            //if (vatNumber.IsUsingCustomValidation)
            //{
            //    return _genericVatValidationServiceBase.ValidateVatNumber(vatNumber);
            //}

            var countryCode = vatNumber.CountryIso2Code.ToUpper();

            if (_validationServicesByCountry.ContainsKey(countryCode))
            {
                return _validationServicesByCountry[countryCode].ValidateVatNumber(vatNumber);
            }

            var validationService = GetCountryValidationServiceType(countryCode);
            if (validationService != null)
            {
                return validationService.ValidateVatNumber(vatNumber);
            }

            return ValidationResults.CountryNotFoundValidationResult;
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
}
