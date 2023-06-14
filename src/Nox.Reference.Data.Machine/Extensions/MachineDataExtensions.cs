using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data.Common;
using Nox.Reference.Data.Machine;

namespace Nox.Reference;

public static class MachineDataExtensions
{
    /// <summary>
    /// This method setups machine context dependencies
    /// </summary>
    /// <param name="services">Current service collection</param>
    /// <returns>Modified service collection</returns>
    public static IServiceCollection AddMachineContext(this IServiceCollection services)
    {
        services.AddNoxReferenceCommon();

        services.AddDbContext<MachineDbContext>();
        services.AddScoped<INoxReferenceDataSeeder, MacAddressDataSeeder>();
        services.AddScoped<IMachineInfoContext, MachineDbContext>();
        return services;
    }
}