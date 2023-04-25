using Nox.Reference.Abstractions;
using Nox.Reference.Data.World;

namespace Nox.Reference.Data;

public static class WorldQueryExtensions
{
    public static ICurrencyInfo Get(this IQueryable<ICurrencyInfo> query, string currency)
    {
        return query.First(x => x.IsoCode == currency);
    }

    public static ICurrencyInfo? GetByIsoCode(this IQueryable<ICurrencyInfo> query, string isoCode)
    {
        return query.FirstOrDefault(x => x.IsoCode == isoCode);
    }

    public static ICurrencyInfo? GetByIsoNumber(this IQueryable<ICurrencyInfo> query, string isoNumber)
    {
        return query.FirstOrDefault(x => x.IsoNumber == isoNumber);
    }

    public static IVatNumberValidationResult? Validate(
        this IVatNumberDefinitionInfo info,
        string validationNumber,
        string country,
        bool shouldValidateViaApi = true)
    {
        return VatNumberService.ValidateVatNumber(info, validationNumber, country, shouldValidateViaApi);
    }
}