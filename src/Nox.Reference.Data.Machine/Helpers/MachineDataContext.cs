using Microsoft.Extensions.DependencyInjection;

namespace Nox.Reference.Data.Machine;

public class MachineDataContext
{
    public static IMachineInfoContext Create()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMachineContext();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        return serviceProvider.GetRequiredService<IMachineInfoContext>();
    }
}
