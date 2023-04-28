using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.World;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data;

public static class WorldDataExtensions
{
    public static IServiceCollection AddWorldContext(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        var configuration = new ConfigurationBuilder()
          .AddJsonFile(ConfigurationConstants.WorldConfigFileName)
          .Build();

        services.AddNoxReferenceCommon();
        services.AddNoxReferenceConfiguration(configuration);

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        var connectionString = configuration.GetConnectionString(ConfigurationConstants.WorldConnectionStringName);
        services.AddSqlite<WorldDbContext>(connectionString);

        services.AddScoped<INoxReferenceDataSeeder, CurrencyDataSeeder>();
        services.AddScoped<INoxReferenceDataSeeder, CultureDataSeeder>();
        services.AddScoped<INoxReferenceDataSeeder, TimeZoneDataSeeder>();
        services.AddScoped<IWorldInfoContext, WorldDbContext>();

        return services;
    }
}