using System.Net;
using System.Numerics;
using Nox.Reference.Data.IpAddress;
using Nox.Reference.Data.IpAddress.Constants;

namespace Nox.Reference;

/// <summary>
/// Service to lookup country code by provided IP address.
/// </summary>
internal class IpAddressService : IIpAddressService
{
    private readonly IpAddressDbContext _dbContext;

    /// <summary>
    /// IpAddressService constructor.
    /// </summary>
    /// <param name="dbContext">IpAddressDbContext type</param>
    public IpAddressService(IpAddressDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// <summary>
    /// Get country code for ip address.
    /// </summary>
    /// <param name="ipAddress">Ipv4 or Ipv6 address string</param>
    /// <returns>IpSearchResult</returns>
    /// </summary>
    public IpSearchResult GetCountryByIPAddress(string ipAddress)
    {
        var validationResult = IpAddressConstants.IpAddressRegex.Match(ipAddress);
        if (!validationResult.Success)
        {
            return IpSearchResult.IncorrectInput();
        }

        var ipAddressInfo = IPAddress.Parse(ipAddress);

        return GetCountryByIPAddress(ipAddressInfo);
    }

    /// <summary>
    /// <summary>
    /// Gets country code for ip address by IPAddress object.
    /// </summary>
    /// <param name="ipAddress">.Net type IPAddress</param>
    /// <returns>IpSearchResult</returns>
    /// </summary>
    public IpSearchResult GetCountryByIPAddress(IPAddress ipAddress)
    {
        var ipAddressBytes = ipAddress.GetAddressBytes();
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(ipAddressBytes);
        }

        var ipAddressLong = new BigInteger(ipAddressBytes, true);

        return GetCountryByIPAddress(ipAddressLong);
    }

    /// <summary>
    /// Gets country code for ip address by IPv4 or IPv6 string.
    /// </summary>
    /// <param name="ipAddressNumber">IPAddress 32 or 128-bit number</param>
    /// <returns></returns>
    public IpSearchResult GetCountryByIPAddress(BigInteger ipAddressNumber)
    {
        var searchChunk = IpAddressChunk.CreateIpAddressChunkFromNumber(ipAddressNumber);

        var ipAddressRange = _dbContext
            .IpAddresses
            .FirstOrDefault(
                x => x.StartAddress.Start <= searchChunk.Start
                && (searchChunk.End == -1 || x.StartAddress.End <= searchChunk.End)
                && x.EndAddress.Start >= searchChunk.Start
                && (x.EndAddress.End == -1 || (x.EndAddress.End >= searchChunk.End))
            );

        if (ipAddressRange == null)
        {
            return IpSearchResult.NotFound();
        }

        return IpSearchResult.Success(ipAddressRange.CountryCode);
    }
}