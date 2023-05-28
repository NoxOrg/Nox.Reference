using Microsoft.Extensions.DependencyInjection;

namespace Nox.Reference.Data.Machine;

public static class Machine
{
    internal static IMachineInfoContext Create()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMachineContext();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        return serviceProvider.GetRequiredService<IMachineInfoContext>();
    }

    public static IQueryable<MacAddress> MacAddresses
         => Create().MacAddresses;
}
