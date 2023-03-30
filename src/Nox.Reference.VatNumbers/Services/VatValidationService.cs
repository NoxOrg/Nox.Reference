using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Services
{
    public class VatValidationService : IVatValidationService
    {
        // TODO: auto-detect by name via reflection
        private readonly IVatValidationService _colombiaValidationService = new ColumbiaValidatorService();
        private readonly IVatValidationService _southAfricaValidationService = new SouthAfricaValidationService();
        private readonly IVatValidationService _ukraineValidationService = new UkraineValidationService(); 
        private readonly IVatValidationService _switzerlandValidationService = new SwitzerlandValidationService();
        private readonly IVatValidationService _brazilValidationService = new BrazilValidationService();
        private readonly IVatValidationService _greatBritainValidationService = new GreatBritainValidationService();
        private readonly IVatValidationService _italyValidationService = new ItalyValidationService();
        private readonly IVatValidationService _franceValidationService = new FranceValidationService();
        private readonly IVatValidationService _germanyValidationService = new GermanyValidationService();
        private readonly IVatValidationService _spainValidationService = new SpainValidationService();
        private readonly IVatValidationService _neitherlandsValidationService = new NeitherlandsValidationService();
        private readonly IVatValidationService _mexicoValidationService = new MexicoValidationService();

        public ValidationResult ValidateVatNumber(IVatNumber vatNumber)
        {
            if (vatNumber == null)
            {
                return ValidationResults.NullValidationResult;
            }

            var result = ValidateVatNumberInternal(vatNumber);

            if (result.ValidationStatus == ValidationStatus.Invalid &&
                vatNumber.Number.Length > 1 &&
                vatNumber.Number.Substring(0, 2) != vatNumber.CountryAlphaCode2)
            {
                var attemptToGuessDifferentCountryResult = ValidateVatNumberInternal(new VatNumber(vatNumber.Number, vatNumber.Number.Substring(0, 2)));
                if (attemptToGuessDifferentCountryResult.ValidationStatus == ValidationStatus.Valid)
                {
                    // TODO: should we handle this 'Validated for different country' case?
                    return attemptToGuessDifferentCountryResult;
                }
            }

            return result;
        }

        private ValidationResult ValidateVatNumberInternal(IVatNumber vatNumber)
        {
            if (vatNumber == null)
            {
                return ValidationResults.NullValidationResult;
            }

            switch (vatNumber.CountryAlphaCode2.ToUpper())
            {
                case "CO":
                    return _colombiaValidationService.ValidateVatNumber(vatNumber);
                case "ZA":
                    return _southAfricaValidationService.ValidateVatNumber(vatNumber);
                case "UA":
                    return _ukraineValidationService.ValidateVatNumber(vatNumber);
                case "CH":
                    return _switzerlandValidationService.ValidateVatNumber(vatNumber);
                case "BR":
                    return _brazilValidationService.ValidateVatNumber(vatNumber);
                case "GB":
                    return _greatBritainValidationService.ValidateVatNumber(vatNumber);
                case "IT":
                    return _italyValidationService.ValidateVatNumber(vatNumber);
                case "FR":
                    return _franceValidationService.ValidateVatNumber(vatNumber);
                case "DE":
                    return _germanyValidationService.ValidateVatNumber(vatNumber);
                case "ES":
                    return _spainValidationService.ValidateVatNumber(vatNumber);
                case "NL":
                    return _neitherlandsValidationService.ValidateVatNumber(vatNumber);
                case "MX":
                    return _mexicoValidationService.ValidateVatNumber(vatNumber);
                default:
                    return ValidationResults.CountryNotFoundValidationResult;
            }
        }
    }
}
