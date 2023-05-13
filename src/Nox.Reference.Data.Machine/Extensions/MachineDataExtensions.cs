using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.Common;
using Nox.Reference.Data.Machine;

namespace Nox.Reference.Data;

public static class MachineDataExtensions
{
    public static IServiceCollection AddMachineContext(this IServiceCollection services)
    {
        services.AddNoxReferenceCommon();

        services.AddDbContext<MachineDbContext>();

        services.AddScoped<INoxReferenceDataSeeder, MacAddressDataSeeder>();
        services.AddScoped<IMachineInfoContext, MachineDbContext>();
        return services;
    }
}