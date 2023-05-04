using Microsoft.Extensions.DependencyInjection;

namespace Nox.Reference.Data.World;

public static class WorldDataContext
{
    public static IWorldInfoContext Create()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddWorldContext();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        return serviceProvider.GetRequiredService<IWorldInfoContext>();
    }
}
