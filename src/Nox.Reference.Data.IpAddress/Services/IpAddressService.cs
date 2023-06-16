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
    public IpSearchResult GetCountryByIp(string ipAddress)
    {
        var validationResult = IpAddressConstants.IpAddressRegex.Match(ipAddress);
        if (!validationResult.Success)
        {
            return IpSearchResult.IncorrectInput();
        }

        var ipAddressInfo = System.Net.IPAddress.Parse(ipAddress);
        var ipAddressBytes = ipAddressInfo.GetAddressBytes();
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(ipAddressBytes);
        }

        var ipAddressLong = new BigInteger(ipAddressBytes, true);
        var searchChunk = IpAddressChunk.CreateIpAddressChunkFromNumber(ipAddressLong);

        var ipAddressRange = _dbContext
            .IpAddresses
            .FirstOrDefault(
                x => x.StartAddress.Start <= searchChunk.Start
                && x.StartAddress.End <= searchChunk.End
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