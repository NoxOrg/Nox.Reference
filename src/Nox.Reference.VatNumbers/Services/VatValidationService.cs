using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers.Services
{
    public class VatValidationService : VatValidationServiceBase
    {
        // TODO: auto-detect by name via reflection
        private readonly VatValidationServiceBase _colombiaValidationService = new ColombiaValidatorService();
        private readonly VatValidationServiceBase _southAfricaValidationService = new SouthAfricaValidationService();
        private readonly VatValidationServiceBase _ukraineValidationService = new UkraineValidationService(); 
        private readonly VatValidationServiceBase _switzerlandValidationService = new SwitzerlandValidationService();
        private readonly VatValidationServiceBase _brazilValidationService = new BrazilValidationService();
        private readonly VatValidationServiceBase _greatBritainValidationService = new GreatBritainValidationService();
        private readonly VatValidationServiceBase _italyValidationService = new ItalyValidationService();
        private readonly VatValidationServiceBase _franceValidationService = new FranceValidationService();
        private readonly VatValidationServiceBase _germanyValidationService = new GermanyValidationService();
        private readonly VatValidationServiceBase _spainValidationService = new SpainValidationService();
        private readonly VatValidationServiceBase _neitherlandsValidationService = new NeitherlandsValidationService();
        private readonly VatValidationServiceBase _mexicoValidationService = new MexicoValidationService();
        private readonly VatValidationServiceBase _indiaValidationService = new IndiaValidationService();
        private readonly VatValidationServiceBase _canadaValidationService = new CanadaValidationService();
        private readonly VatValidationServiceBase _belgiumValidationService = new BelgiumValidationService();
        private readonly VatValidationServiceBase _australiaValidationService = new AustraliaValidationService();
        private readonly VatValidationServiceBase _polandValidationService = new PolandValidationService();
        private readonly VatValidationServiceBase _portugalValidationService = new PortugalValidationService();

        public override ValidationResult ValidateVatNumber(IVatNumber vatNumber)
        {
            if (vatNumber == null)
            {
                return ValidationResults.NullValidationResult;
            }

            var result = ValidateVatNumberInternal(vatNumber);

            // TODO: Issue #12: We could optionally auto-detect and validate country here in case original validation failed

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
                case "IN":
                    return _indiaValidationService.ValidateVatNumber(vatNumber);
                case "CA":
                    return _canadaValidationService.ValidateVatNumber(vatNumber);
                case "BE":
                    return _belgiumValidationService.ValidateVatNumber(vatNumber);
                case "AU":
                    return _australiaValidationService.ValidateVatNumber(vatNumber);
                case "PL":
                    return _polandValidationService.ValidateVatNumber(vatNumber);
                case "PT":
                    return _portugalValidationService.ValidateVatNumber(vatNumber);
                default:
                    return ValidationResults.CountryNotFoundValidationResult;
            }
        }
    }
}
