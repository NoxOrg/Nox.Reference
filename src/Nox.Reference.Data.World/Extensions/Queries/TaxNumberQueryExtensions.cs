namespace Nox.Reference;

public static class TaxNumberQueryExtensions
{
    /// <summary>
    /// Get tax number definition by country Alpha 2 code
    /// <example>
    /// <code>
    /// TaxNumberDefinitions.Get("UA")
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="country">Country alpha 2 code. Example: "UA".</param>
    /// <returns>Resulting country</returns>
    public static TaxNumberDefinition? Get(this IQueryable<TaxNumberDefinition> query, string country)
    {
        return query.FirstOrDefault(x => x.Country.AlphaCode2.ToUpper() == country.ToUpper());
    }

    /// <summary>
    /// Get tax number definition by country
    /// <example>
    /// <code>
    /// TaxNumberDefinitions.Get(WorldCountries.France)
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="country">Country enum. Example: WorldCountries.France.</param>
    /// <returns>Resulting country</returns>
    public static TaxNumberDefinition? Get(this IQueryable<TaxNumberDefinition> query, WorldCountries country)
    {
        return query.FirstOrDefault(x => x.Country.Name == EnumHelper.GetItemDescription(country));
    }

    /// <summary>
    /// Validate tax number
    /// <example>
    /// <code>
    /// TaxNumberDefinitions.Validate("UA", "UA0203654090", false)
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="countryAlpha2Code">Alpha 2 country code. Example: "UA".</param>
    /// <param name="validationNumber">Tax number as string</param>
    /// <param name="shouldValidateViaApi">Flag to determine if validation should use online API service (if applicable) or not</param>
    /// <returns>Validation result</returns>
    public static TaxNumberValidationResult? Validate(
        this IQueryable<TaxNumberDefinition> query,
        string countryAlpha2Code,
        string validationNumber,
        bool shouldValidateViaApi = true)
    {
        var definition = query.FirstOrDefault(x => x.Country.AlphaCode2.ToUpper() == countryAlpha2Code.ToUpper());

        if (definition == null)
        {
            throw new Exception($"Country {countryAlpha2Code} is not supported.");
        }

        return definition.Validate(validationNumber, shouldValidateViaApi);
    }

    /// <summary>
    /// Validate tax number
    /// <example>
    /// <code>
    /// TaxNumberDefinitions.Validate(WorldCountries.France, "UA0203654090", false)
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="query">Current collection</param>
    /// <param name="country">Country enum. Example: WorldCountries.France.</param>
    /// <param name="validationNumber">Tax number as string</param>
    /// <param name="shouldValidateViaApi">Flag to determine if validation should use online API service (if applicable) or not</param>
    /// <returns>Validation result</returns>
    public static TaxNumberValidationResult? Validate(
        this IQueryable<TaxNumberDefinition> query,
        WorldCountries country,
        string validationNumber,
        bool shouldValidateViaApi = true)
    {
        var definition = query.FirstOrDefault(x => x.Country.Name == EnumHelper.GetItemDescription(country));

        if (definition == null)
        {
            throw new Exception($"Country {country} is not supported.");
        }

        return definition.Validate(validationNumber, shouldValidateViaApi);
    }

    /// <summary>
    /// Validate tax number
    /// <example>
    /// <code>
    /// definition.Validate("UA0203600000", false)
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="info">Tax number definition with validation info</param>
    /// <param name="validationNumber">Tax number as string. Example: "UA0203600000"</param>
    /// <param name="shouldValidateViaApi">Flag to determine if validation should use online API service (if applicable) or not</param>
    /// <returns>Validation result</returns>
    public static TaxNumberValidationResult? Validate(
        this TaxNumberDefinition info,
        string validationNumber,
        bool shouldValidateViaApi = true)
    {
        return TaxValidationService.ValidateTaxNumber(info, validationNumber, shouldValidateViaApi);
    }
}