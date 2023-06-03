using Nox.Reference.Data.World.Models;
using Nox.Reference.Data.World.Services.VatNumbers;

namespace Nox.Reference.Data.World.Extensions.Queries;

public static class VatNumberQueryExtensions
{
    /// <summary>
    /// Get vat number definition by country Alpha 2 code
    /// <example>
    /// <code>
    /// VatNumberDefinitions.Get("UA")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="country">Country alpha 2 code. Example: "UA".</param>
    /// <returns>Resulting country</returns>
    public static VatNumberDefinition? Get(this IQueryable<VatNumberDefinition> query, string country)
    {
        return query.FirstOrDefault(x => x.Country.Equals(country, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Validate vat number
    /// <example>
    /// <code>
    /// VatNumberDefinitions.Validate("UA", "UA0203654090", false)
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="country">Alpha 2 country code. Example: "UA".</param>
    /// <param name="validationNumber">Vat number as string</param>
    /// <param name="shouldValidateViaApi">Flag to determine if validation should use online API service (if applicable) or not</param>
    /// <returns>Validation result</returns>
    public static VatNumberValidationResult? Validate(
        this IQueryable<VatNumberDefinition> query,
        string country,
        string validationNumber,
        bool shouldValidateViaApi = true)
    {
        var definition = query.FirstOrDefault(x => x.Country.Equals(country, StringComparison.OrdinalIgnoreCase));

        if (definition == null)
        {
            throw new Exception($"Country {country} is not supported.");
        }

        return definition.Validate(validationNumber, shouldValidateViaApi);
    }

    /// <summary>
    /// Validate vat number
    /// <example>
    /// <code>
    /// definition.Validate("UA0203600000", false)
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="info">Vat number definition with validation info</param>
    /// <param name="validationNumber">Vat number as string. Example: "UA0203600000"</param>
    /// <param name="shouldValidateViaApi">Flag to determine if validation should use online API service (if applicable) or not</param>
    /// <returns>Validation result</returns>
    public static VatNumberValidationResult? Validate(
        this VatNumberDefinition info,
        string validationNumber,
        bool shouldValidateViaApi = true)
    {
        return VatValidationService.ValidateVatNumber(info, validationNumber, shouldValidateViaApi);
    }
}