using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data.IpAddress;

namespace Nox.Reference;

public static class IpAddressContext
{
    public static readonly IIpAddressService _ipAddressService;

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
    /// Get country code for ip address.
    /// </summary>
    /// <param name="ipAddress">Ipv4 or Ipv6 address string</param>
    /// <returns>Two letter Country Code</returns>
    /// <exception cref="ApplicationException"></exception>
    public static IpSearchResult GetCountryByIp(string ipAddress)
    {
        return _ipAddressService.GetCountryByIp(ipAddress);
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