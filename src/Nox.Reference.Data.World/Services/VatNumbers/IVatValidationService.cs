using Nox.Reference.Data.World.Models;

namespace Nox.Reference.Data.World.Services.VatNumbers;

public interface IVatValidationService
{
    VatNumberValidationResult ValidateVatNumber(
        string vatNumber,
        bool shouldValidateViaApi = true);

    string CountryCode { get; }
}