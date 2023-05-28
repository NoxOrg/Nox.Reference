using Nox.Reference.Data.World.Models;
using Nox.Reference.Data.World.Services.VatNumbers;

namespace Nox.Reference.Data.World.Extensions.Queries;

public static class VatNumberQueryExtensions
{
    public static VatNumberDefinition? Get(this IQueryable<VatNumberDefinition> query, string country)
    {
        return query.FirstOrDefault(x => x.Country == country);
    }

    public static VatNumberValidationResult? Validate(
        this IQueryable<VatNumberDefinition> query,
        string country,
        string validationNumber,
        bool shouldValidateViaApi = true)
    {
        var definition = query.FirstOrDefault(x => x.Country == country);

        if (definition == null)
        {
            throw new Exception($"Country {country} is not supported.");
        }

        return definition.Validate(validationNumber, shouldValidateViaApi);
    }

    public static VatNumberValidationResult? Validate(
        this VatNumberDefinition info,
        string validationNumber,
        bool shouldValidateViaApi = true)
    {
        return VatValidationService.ValidateVatNumber(info, validationNumber, shouldValidateViaApi);
    }
}