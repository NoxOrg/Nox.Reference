using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

internal static class VatNumberService
{
    public static IVatNumberValidationResult ValidateVatNumber(
        IVatNumberDefinitionInfo vatNumberDefinitionInfo,
        string vatNumber,
        string country,
        bool shouldValidateViaApi = true)
    {
        if (string.IsNullOrWhiteSpace(vatNumber))
        {
            return VatNumberValidationResult.CreateWithoutValidation(ValidationErrors.EmptyVatNumberError);
        }

        if (vatNumberDefinitionInfo == null)
        {
            return VatNumberValidationResult.CreateWithoutValidation(ValidationErrors.ValidatorNotFoundError);
        }

        var isSupportCountry = VatValidationService.IsSupportingCountryValidation(country);

        if (isSupportCountry)
        {
            return VatValidationService.ValidateVatNumber(vatNumber, vatNumberDefinitionInfo, shouldValidateViaApi);
        }

        return VatNumberValidationResult.CreateWithValidaton(vatNumber, country);
    }
}