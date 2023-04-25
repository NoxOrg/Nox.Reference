using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

public abstract class VatValidationServiceBase
{
    public IVatNumberValidationResult ValidateVatNumber(
        string vatNumber,
        IVatNumberDefinitionInfo vatNumberInfo,
        bool shouldValidateViaApi = true)
    {
        var validationResult = DoValidateVatNumber(vatNumber, vatNumberInfo, shouldValidateViaApi);

        return validationResult;
    }

    protected abstract IVatNumberValidationResult DoValidateVatNumber(
        string vatNumber,
        IVatNumberDefinitionInfo vatNumberInfo,
        bool shouldValidateViaApi = true);
}