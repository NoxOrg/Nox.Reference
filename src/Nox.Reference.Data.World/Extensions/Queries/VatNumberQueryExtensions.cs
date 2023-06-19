namespace Nox.Reference;

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
    /// <param name="query">Current collection</param>
    /// <param name="country">Country alpha 2 code. Example: "UA".</param>
    /// <returns>Resulting country</returns>
    public static VatNumberDefinition? Get(this IQueryable<VatNumberDefinition> query, string country)
    {
        return query.FirstOrDefault(x => x.Country.AlphaCode2.ToUpper() == country.ToUpper());
    }

    /// <summary>
    /// Get vat number definition by country
    /// <example>
    /// <code>
    /// VatNumberDefinitions.Get(WorldCountries.France)
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="country">Country enum. Example: WorldCountries.France.</param>
    /// <returns>Resulting country</returns>
    public static VatNumberDefinition? Get(this IQueryable<VatNumberDefinition> query, WorldCountries country)
    {
        return query.FirstOrDefault(x => x.Country.Name == EnumHelper.GetItemDescription(country));
    }

    /// <summary>
    /// Validate vat number
    /// <example>
    /// <code>
    /// VatNumberDefinitions.Validate("UA", "UA0203654090", false)
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="countryAlpha2Code">Alpha 2 country code. Example: "UA".</param>
    /// <param name="validationNumber">Vat number as string</param>
    /// <param name="shouldValidateViaApi">Flag to determine if validation should use online API service (if applicable) or not</param>
    /// <returns>Validation result</returns>
    public static VatNumberValidationResult? Validate(
        this IQueryable<VatNumberDefinition> query,
        string countryAlpha2Code,
        string validationNumber,
        bool shouldValidateViaApi)
    {
        var definition = query.FirstOrDefault(x => x.Country.AlphaCode2.ToUpper() == countryAlpha2Code.ToUpper());

        if (definition == null)
        {
            throw new Exception($"Country {countryAlpha2Code} is not supported.");
        }

        return definition.Validate(validationNumber, shouldValidateViaApi);
    }

    /// <summary>
    /// Validate vat number
    /// <example>
    /// <code>
    /// VatNumberDefinitions.Validate(WorldCountries.France, "UA0203654090", false)
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="country">Country enum. Example: WorldCountries.France.</param>
    /// <param name="validationNumber">Vat number as string</param>
    /// <param name="shouldValidateViaApi">Flag to determine if validation should use online API service (if applicable) or not</param>
    /// <returns>Validation result</returns>
    public static VatNumberValidationResult? Validate(
        this IQueryable<VatNumberDefinition> query,
        WorldCountries country,
        string validationNumber,
        bool shouldValidateViaApi)
    {
        var definition = query.FirstOrDefault(x => x.Country.Name == EnumHelper.GetItemDescription(country));

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
        bool shouldValidateViaApi)
    {
        return VatValidationService.ValidateVatNumber(info, validationNumber, shouldValidateViaApi);
    }
}