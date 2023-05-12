using Nox.Reference.Data.World.Models;

namespace Nox.Reference.Data.World;

public interface IVatValidationService
{
    VatNumberValidationResult ValidateVatNumber(
        string vatNumber,
        bool shouldValidateViaApi = true);

    string CountryCode { get; }
}