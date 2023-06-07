using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Nox.Reference.Data.Machine;

public static class Machine
{
    private static readonly IServiceProvider _serviceProvider;
    public static readonly IMapper Mapper;

    static Machine()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMachineContext();

        _serviceProvider = serviceCollection.BuildServiceProvider();
        Mapper = _serviceProvider.GetRequiredService<IMapper>();
    }

    public static IQueryable<MacAddress> MacAddresses
        => WorldDataContext.MacAddresses;

    private static IMachineInfoContext WorldDataContext
        => _serviceProvider.GetRequiredService<IMachineInfoContext>();

    /// <summary>
    /// <para>Override default database path. Examples: </para>
    /// <para>'Data Source=.\NoxReferenceDatabase\Nox.Reference.Machine.db'</para>
    /// <para>'Data Source=..\..\data\Nox.Reference.Machine.db'</para>
    /// <para>'Data Source=C:\project\NoxReferenceDatabase\Nox.Reference.Machine.db'</para>
    /// </summary>
    /// <param name="databasePath">New overridden database connection string</param>
    public static void UseDatabaseConnectionString(string path)
        => MachineDbContext.UseDatabaseConnectionString(path);
}
