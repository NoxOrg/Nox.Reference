using Microsoft.Extensions.DependencyInjection;

namespace Nox.Reference.Data.World;

public static class WorldDataContext
{
    public static IWorldInfoContext Create(string connectionStringKey = null)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddWorldContext(connectionStringKey);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        return serviceProvider.GetRequiredService<IWorldInfoContext>();
    }
}
