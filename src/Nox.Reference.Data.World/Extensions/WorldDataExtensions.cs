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

        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile(ConfigurationConstants.ConfigFileName)
            .Build();

        services.AddScoped(_ => configuration);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        var connectionString = configuration.GetConnectionString(ConfigurationConstants.ConnectionStringName);
        services.AddSqlite<WorldDbContext>(connectionString);

        services.AddScoped<INoxReferenceDataSeeder, CurrencyDataSeeder>();
        services.AddScoped<IWorldInfoContext, WorldDbContext>();
        return services;
    }
}