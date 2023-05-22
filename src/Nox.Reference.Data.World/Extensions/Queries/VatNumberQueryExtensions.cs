using Nox.Reference.Data.World.Models;

namespace Nox.Reference.Data.World.Extensions.Queries;

public static class VatNumberQueryExtensions
{
    public static VatNumberDefinition? Get(this IQueryable<VatNumberDefinition> query, string country)
    {
        return query.FirstOrDefault(x => x.Country == country);
    }

    public static VatNumberValidationResult? Validate(
        this VatNumberDefinition info,
        string validationNumber,
        bool shouldValidateViaApi = true)
    {
        return VatValidationService.ValidateVatNumber(info, validationNumber, shouldValidateViaApi);
    }
}