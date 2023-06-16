using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.Common;
using Nox.Reference.Data.IpAddress;
using Nox.Reference.Data.IpAddress.DataSeeds;

namespace Nox.Reference.Data;

public static class IpAddressDataExtensions
{
    /// <summary>
    /// This method setups machine context dependencies
    /// </summary>
    /// <param name="services">Current service collection</param>
    /// <returns>Modified service collection</returns>
    public static IServiceCollection AddIpAddressContext(this IServiceCollection services)
    {
        services.AddNoxReferenceCommon();

        services.AddDbContext<IpAddressDbContext>();

        services.AddScoped<IpAddressService>();

        services.AddScoped<INoxReferenceDataSeeder, IpAddressDataSeeder>();
        services.AddScoped<IIpAddressInfoContext, IpAddressDbContext>();

        return services;
    }
}