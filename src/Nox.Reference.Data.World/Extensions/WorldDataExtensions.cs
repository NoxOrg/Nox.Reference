using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.World;
using Nox.Reference.PhoneNumbers;

namespace Nox.Reference.Data;

public static class WorldDataExtensions
{
    public static IServiceCollection AddWorldContext(this IServiceCollection services, string connectionStringKey = null)
    {
        services.AddNoxReferenceCommon();

        var configuration = services.GetNoxReferenceConfiguration();

        var connectionString = configuration.GetConnectionString(connectionStringKey ?? ConfigurationConstants.WorldConnectionStringName);
        services.AddSqlite<WorldDbContext>(connectionString);

        services.AddSeeders();
        services.AddScoped<IWorldInfoContext, WorldDbContext>();

        services.AddSingleton<IPhoneNumberService, PhoneNumberService>();

        return services;
    }
}