using Nox.Reference.Abstractions;
using Nox.Reference.Common;

namespace Nox.Reference.Data.World;

internal class VatNumberService
{
    private readonly WorldDbContext _dbContext;

    public VatNumberService(WorldDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IVatNumberValidationResult ValidateVatNumber(
        string vatNumber,
        string country,
        bool shouldValidateViaApi = true)
    {
        if (string.IsNullOrWhiteSpace(vatNumber))
        {
            return VatNumberValidationResult.CreateWithoutValidation(ValidationErrors.EmptyVatNumberError);
        }

        var vatNumberDefinition = _dbContext.VatNumberDefinitions.FirstOrDefault(x => x.Country == country);

        if (vatNumberDefinition == null)
        {
            return VatNumberValidationResult.CreateWithoutValidation(ValidationErrors.ValidatorNotFoundError);
        }

        var isSupportCountry = VatValidationService.IsSupportingCountryValidation(country);

        if (isSupportCountry)
        {
            return VatValidationService.ValidateVatNumber(vatNumber, vatNumberDefinition, shouldValidateViaApi);
        }

        return VatNumberValidationResult.CreateWithValidaton(vatNumber.NormalizeVatNumber(country));
    }
}