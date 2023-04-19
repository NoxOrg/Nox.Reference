using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.Common;
using Nox.Reference.MacAddresses.DataContext;

namespace Nox.Reference.Data.Extensions;

public static class MacAddressDataExtensions
{
    public static IServiceCollection AddMacAddressDbContext(this IServiceCollection services)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsetings.json")
            .Build();

        services.AddScoped(_ => configuration);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        var connectionString = configuration.GetConnectionString(ConfigurationConstants.ConnectionStringName);
        services.AddSqlite<MacAddressDbContext>(connectionString);

        services.AddScoped<INoxReferenceDataSeeder, MacAddressDataSeeder>();
        services.AddScoped<IMacAddressContext, MacAddressDbContext>();
        return services;
    }
}