using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.Common;
using Nox.Reference.Data.Machine;

namespace Nox.Reference.Data;

public static class MachineDataExtensions
{
    public static IServiceCollection AddMachineContext(this IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(ConfigurationConstants.MachineConfigFileName)
            .Build();

        services.AddNoxReferenceCommon();

        services.AddNoxReferenceConfiguration(configuration);

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        var connectionString = configuration.GetConnectionString(ConfigurationConstants.MachineConnectionStringName);
        services.AddSqlite<MachineDbContext>(connectionString);

        services.AddScoped<INoxReferenceDataSeeder, MacAddressDataSeeder>();
        services.AddScoped<IMachineContext, MachineDbContext>();
        return services;
    }
}