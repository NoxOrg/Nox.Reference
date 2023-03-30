using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.MacAddresses.Models;

namespace Nox.Reference.MacAddresses;

public static class AppMacAddressExtensions
{
    private const string ResourceName = "Nox.Reference.MacAddresses.json";

    public static IServiceCollection AddNoxMacAddresses(this IServiceCollection services)
    {
        InitMacAddressService();

        services.AddScoped(typeof(LookupHandler<>));
        services.AddScoped<IMacAddressService, MacAddressService>();

        return services;
    }

    private static void InitMacAddressService()
    {
        var macAddresses = AssemblyDataInitializer.GetDataFromAssemblyResource<MacAddressInfo>(ResourceName);
        MacAddressService.Init(macAddresses);
    }
}