using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.MacAddresses.Models;

namespace Nox.Reference.MacAddresses;

public static class AppMacAddressExtensions
{
    private const string ResourceName = "Nox.Reference.MacAddresses.json";
    private static readonly object _syncObj = new();
    private static bool _initialized = false;

    public static IServiceCollection AddNoxMacAddresses(this IServiceCollection services)
    {
        InitMacAddressService();

        services.AddScoped<IMacAddressService, MacAddressService>();

        return services;
    }

    private static void InitMacAddressService()
    {
        lock (_syncObj)
        {
            if (_initialized)
            {
                return;
            }

            var macAddresses = AssemblyDataInitializer.GetDataFromAssemblyResource<MacAddressInfo>(ResourceName);
            MacAddressService.Init(macAddresses);

            _initialized = true;
        }
    }
}