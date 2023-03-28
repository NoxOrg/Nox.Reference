using Microsoft.Extensions.DependencyInjection;

namespace Nox.Reference.MacAddresses;

public static class AppMacAddressExtensions
{
    public static IServiceCollection AddMacAddresses(this IServiceCollection services)
    {
        services.AddSingleton(_ => CreateMacAddressService());

        return services;
    }

    private static IMacAddressService CreateMacAddressService()
    {
        var macAddressInitializer = new MacAddressInitializer();
        var macAddresses = macAddressInitializer.GetDataFromAssemblyResource();

        var macAddressService = new MacAddressService(macAddresses);

        return macAddressService;
    }
}