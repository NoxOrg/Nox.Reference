namespace Nox.Reference;

public class IpSearchResult
{
    /// <summary>
    /// Indicates Result type.
    /// </summary>
    public IpSearchResultKind Kind { get; private set; }

    /// <summary>
    /// Gets CountryCode in case search was successful.
    /// </summary>
    public string? CountryCode { get; private set; }

    /// <summary>
    /// Creates Success result. Kind = Success
    /// </summary>
    /// <param name="countryCode">Country Code</param>
    /// <returns></returns>
    public static IpSearchResult Success(string countryCode)
        => new()
        {
            Kind = IpSearchResultKind.Success,
            CountryCode = countryCode
        };

    /// <summary>
    /// Creates NotFound result. Kind = NotFound
    /// </summary>
    /// <returns></returns>
    public static IpSearchResult NotFound()
        => new()
        {
            Kind = IpSearchResultKind.NotFound
        };

    /// <summary>
    /// Creates IncorrectInput result. Kind = IncorrectInput
    /// </summary>
    /// <returns></returns>
    public static IpSearchResult IncorrectInput()
        => new()
        {
            Kind = IpSearchResultKind.IncorrectInput
        };
}