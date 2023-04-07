using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;

namespace Nox.Reference.MacAddresses;

public static class AppMacAddressExtensions
{
    public static IServiceCollection AddNoxMacAddresses(this IServiceCollection services)
    {
        services.AddScoped<IMacAddressService, MacAddressService>();

        services.AddNoxReferenceCommon();

        return services;
    }
}