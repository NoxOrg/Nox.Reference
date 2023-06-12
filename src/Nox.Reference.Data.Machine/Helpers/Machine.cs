using Microsoft.Extensions.DependencyInjection;

namespace Nox.Reference.Data.Machine;

public static class Machine
{
    private static readonly IServiceProvider _serviceProvider;

    static Machine()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMachineContext();

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    public static IQueryable<MacAddress> MacAddresses
         => WorldDataContext.MacAddresses;

    private static IMachineInfoContext WorldDataContext
        => _serviceProvider.GetRequiredService<IMachineInfoContext>();
}