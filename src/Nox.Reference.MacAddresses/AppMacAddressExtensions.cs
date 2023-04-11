using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data;
using Nox.Reference.Data.Repositories;

namespace Nox.Reference.MacAddresses;

public static class AppMacAddressExtensions
{
    public static IServiceCollection AddNoxMacAddresses(this IServiceCollection services, IConfiguration configuartion)
    {
        services.AddScoped<IMacAddressService, MacAddressService>();
        services.AddNoxReferenceData(configuartion);

        return services;
    }
}