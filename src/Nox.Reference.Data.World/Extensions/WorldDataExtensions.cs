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
        services.AddNoxReferenceCommon();

        var configuration = services.GetNoxReferenceConfiguration();
        var connectionString = configuration.GetConnectionString(ConfigurationConstants.WorldConnectionStringName);
        services.AddSqlite<WorldDbContext>(connectionString);

        services.AddScoped<INoxReferenceDataSeeder, CurrencyDataSeeder>();
        services.AddScoped<INoxReferenceDataSeeder, VatNumberDefinitionDataSeeder>();
        services.AddScoped<IWorldInfoContext, WorldDbContext>();

        return services;
    }
}