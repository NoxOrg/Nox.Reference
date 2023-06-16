namespace Nox.Reference;

/// <summary>
/// Service to lookup country code by provided IP address.
/// </summary>
public interface IIpAddressService
{
    /// <summary>
    /// <summary>
    /// Get country code for ip address.
    /// </summary>
    /// <param name="ipAddress">Ipv4 or Ipv6 address string</param>
    /// <returns>IpSearchResult</returns>
    /// </summary>
    IpSearchResult GetCountryByIp(string ipAddress);
}