namespace Nox.Reference;

/// <summary>
/// Service to lookup country code by provided IP address.
/// </summary>
public interface IIpAddressService
{
    /// <summary>
    /// Get country code for ip address.
    /// </summary>
    /// <param name="ipAddress">Ipv4 or Ipv6 address string</param>
    /// <returns>Two letter Country Code</returns>
    /// <exception cref="ApplicationException"></exception>
    IpSearchResult GetCountryByIp(string ipAddress);
}
