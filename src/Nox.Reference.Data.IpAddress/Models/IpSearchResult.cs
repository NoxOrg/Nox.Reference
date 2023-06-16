namespace Nox.Reference;

public class IpSearchResult
{
    public IpSearchResultKind Kind { get; private set; }
    public string? CountryCode { get; private set; }

    public static IpSearchResult Success(string countryCode)
        => new()
        {
            Kind = IpSearchResultKind.Success,
            CountryCode = countryCode
        };

    public static IpSearchResult NotFound()
        => new()
        {
            Kind = IpSearchResultKind.NotFound
        };
}