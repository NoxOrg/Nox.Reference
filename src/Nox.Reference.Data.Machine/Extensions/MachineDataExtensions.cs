using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.Common;
using Nox.Reference.Data.Machine;

namespace Nox.Reference.Data;

public static class MachineDataExtensions
{
    public static IServiceCollection AddMachineContext(this IServiceCollection services, string connectionStringKey = null)
    {
        services.AddNoxReferenceCommon();

        var configuration = services.GetNoxReferenceConfiguration();
        var connectionString = configuration.GetConnectionString(connectionStringKey ?? ConfigurationConstants.MachineConnectionStringName);

        services.AddSqlite<MachineDbContext>(connectionString);

        services.AddScoped<INoxReferenceDataSeeder, MacAddressDataSeeder>();
        services.AddScoped<IMachineContext, MachineDbContext>();
        return services;
    }
}