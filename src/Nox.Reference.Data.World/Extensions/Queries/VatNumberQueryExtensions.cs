using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World.Extensions.Queries;

public static class VatNumberQueryExtensions
{
    public static IVatNumberDefinitionInfo? Get(this IQueryable<IVatNumberDefinitionInfo> query, string country)
    {
        return query.FirstOrDefault(x => x.Country == country);
    }

    public static IVatNumberValidationResult? Validate(
        this IVatNumberDefinitionInfo info,
        string validationNumber,
        bool shouldValidateViaApi = true)
    {
        return VatValidationService.ValidateVatNumber(info, validationNumber, shouldValidateViaApi);
    }
}