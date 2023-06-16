using System.Numerics;
using System.Text.RegularExpressions;
using Nox.Reference.Data.IpAddress;

namespace Nox.Reference;

/// <summary>
/// Service to lookup country code by provided IP address.
/// </summary>
internal class IpAddressService : IIpAddressService
{
    private readonly Regex _ipAddressRegex = new Regex(@"((^\s*((([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]))\s*$)|(^\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))(%.+)?\s*$))");
    private readonly IpAddressDbContext _dbContext;

    /// <summary>
    /// IpAddressService constructor.
    /// </summary>
    /// <param name="dbContext"></param>
    public IpAddressService(IpAddressDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Get country code for ip address.
    /// </summary>
    /// <param name="ipAddress">Ipv4 or Ipv6 address string</param>
    /// <returns>Two letter Country Code</returns>
    /// <exception cref="ApplicationException"></exception>
    public IpSearchResult GetCountryByIp(string ipAddress)
    {
        var validationResult = _ipAddressRegex.Match(ipAddress);
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