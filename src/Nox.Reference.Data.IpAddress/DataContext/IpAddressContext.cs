using Microsoft.Extensions.DependencyInjection;

namespace Nox.Reference.Data.IpAddress;

public static class IpAddressContext
{
    private static readonly IServiceProvider _serviceProvider;

    static IpAddressContext()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddIpAddressContext();

        _serviceProvider = serviceCollection.BuildServiceProvider();
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
