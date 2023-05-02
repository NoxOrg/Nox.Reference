using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World;

public interface IVatValidationService
{
    IVatNumberValidationResult ValidateVatNumber(
        string vatNumber,
        bool shouldValidateViaApi = true);

    string CountryCode { get; }
}