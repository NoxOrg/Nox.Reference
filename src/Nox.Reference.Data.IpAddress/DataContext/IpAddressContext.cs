using System.Net;
using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data.IpAddress;

namespace Nox.Reference;

/// <summary>
/// Service to lookup country code by provided IP address.
/// </summary>
public static class IpAddressContext
{
    private static readonly IIpAddressService _ipAddressService;

#pragma warning disable S3963 // "static" fields should be initialized inline

    static IpAddressContext()
#pragma warning restore S3963 // "static" fields should be initialized inline
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddIpAddressContext();

        _ipAddressService = serviceCollection
            .BuildServiceProvider()
            .GetRequiredService<IIpAddressService>();
    }

    /// <summary>
    /// <summary>
    /// Gets country code for ip address by IPAddress object.
    /// </summary>
    /// <param name="ipAddress">.Net type IPAddress</param>
    /// <returns>IpSearchResult</returns>
    /// </summary>
    public static IpSearchResult GetCountryByIPAddress(IPAddress ipAddress)
    {
        return _ipAddressService.GetCountryByIPAddress(ipAddress);
    }

    /// <summary>
    /// Gets country code for ip address by IPv4 or ipv6 string.
    /// </summary>
    /// <param name="ipAddress">Ipv4 or Ipv6 address string</param>
    /// <returns></returns>
    public static IpSearchResult GetCountryByIPAddress(string ipAddress)
    {
        return _ipAddressService.GetCountryByIPAddress(ipAddress);
    }

    /// <summary>
    /// Gets country code for ip address by IPv4 or IPv6 string.
    /// </summary>
    /// <param name="ipAddressNumber">IPAddress 32 or 128-bit number</param>
    /// <returns></returns>
    public static IpSearchResult GetCountryByIPAddress(BigInteger ipAddressNumber)
    {
        return _ipAddressService.GetCountryByIPAddress(ipAddressNumber);
    }

    /// <summary>
    /// <para>Override default database path. Examples: </para>
    /// <para>'Data Source=.\NoxReferenceDatabase\Nox.Reference.Machine.db'</para>
    /// <para>'Data Source=..\..\data\Nox.Reference.Machine.db'</para>
    /// <para>'Data Source=C:\project\NoxReferenceDatabase\Nox.Reference.Machine.db'</para>
    /// </summary>
    /// <param name="path">New overridden database connection string</param>
    public static void UseDatabaseConnectionString(string path)
        => IpAddressDbContext.UseDatabaseConnectionString(path);
}