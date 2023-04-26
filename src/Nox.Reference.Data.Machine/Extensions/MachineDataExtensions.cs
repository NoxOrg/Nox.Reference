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
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile(ConfigurationConstants.ConfigFileName)
            .Build();

        services.AddScoped(_ => configuration);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        var connectionString = configuration.GetConnectionString(ConfigurationConstants.ConnectionStringName);
        services.AddSqlite<MachineDbContext>(connectionString);

        services.AddScoped<INoxReferenceDataSeeder, MachineDataSeeder>();
        services.AddScoped<IMachineContext, MachineDbContext>();
        return services;
    }
}