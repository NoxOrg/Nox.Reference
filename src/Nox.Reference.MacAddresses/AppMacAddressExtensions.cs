using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.MacAddresses.Models;

namespace Nox.Reference.MacAddresses;

public static class AppMacAddressExtensions
{
    private const string ResourceName = "Nox.Reference.MacAddresses.json";

    public static IServiceCollection AddNoxMacAddresses(this IServiceCollection services)
    {
        services.AddSingleton(_ => CreateMacAddressService());
        services.AddTransient(typeof(LookupHandler<>));

        return services;
    }

    private static IMacAddressService CreateMacAddressService()
    {
        var macAddresses = AssemblyDataInitializer.GetDataFromAssemblyResource<MacAddressInfo>(ResourceName);
        var macAddressService = new MacAddressService(macAddresses);

        return macAddressService;
    }
}