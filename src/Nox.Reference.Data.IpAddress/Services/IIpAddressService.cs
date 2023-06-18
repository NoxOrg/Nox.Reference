using System.Net;
using System.Numerics;

namespace Nox.Reference;

/// <summary>
/// Service to lookup country code by provided IP address.
/// </summary>
public interface IIpAddressService
{
    /// <summary>
    /// <summary>
    /// Gets country code for ip address by IPAddress object.
    /// </summary>
    /// <param name="ipAddress">.Net type IPAddress</param>
    /// <returns>IpSearchResult</returns>
    /// </summary>
    IpSearchResult GetCountryByIPAddress(IPAddress ipAddress);

    /// <summary>
    /// Gets country code for ip address by IPv4 or ipv6 string.
    /// </summary>
    /// <param name="ipAddress">Ipv4 or Ipv6 address string</param>
    /// <returns></returns>
    IpSearchResult GetCountryByIPAddress(string ipAddress);

    /// <summary>
    /// Gets country code for ip address by IPv4 or IPv6 string.
    /// </summary>
    /// <param name="ipAddressNumber">IPAddress 32 or 128-bit number</param>
    /// <returns></returns>
    IpSearchResult GetCountryByIPAddress(BigInteger ipAddressNumber);
}