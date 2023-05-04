using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.World;

namespace Nox.Reference.Data;

public static class WorldDataExtensions
{
    public static IServiceCollection AddWorldContext(this IServiceCollection services)
    {
        services.AddNoxReferenceCommon();

        var configuration = services.GetNoxReferenceConfiguration();
        CopyDbToFileSystem(configuration);

        var connectionString = configuration.GetConnectionString(ConfigurationConstants.WorldConnectionStringName);
        services.AddSqlite<WorldDbContext>(connectionString);

        services.AddSeeders();
        services.AddScoped<IWorldInfoContext, WorldDbContext>();

        return services;
    }

    /// <summary>
    /// Copy database from nuget to file system so it can be used in app
    /// </summary>
    /// <param name="configuration">App config</param>
    private static void CopyDbToFileSystem(IConfiguration configuration)
    {
        try
        {
            var noxReferenceDbName = configuration.GetValue<string>(ConfigurationConstants.NoxReferenceWorldDbName)!;
            var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var dbName = names.First(x => x.Contains(noxReferenceDbName));
            using var db = Assembly.GetExecutingAssembly().GetManifestResourceStream(dbName)!;
            using var sr = new StreamReader(db);
            using var file = File.Create(noxReferenceDbName);
            sr.BaseStream.Position = 0;
            sr.BaseStream.CopyTo(file);
        }
        catch (IOException ex)
        {
            if (ex.Message.Contains("is being used by another process"))
            {
                // ignore this scenario
                // it can happen when the context is created two separate times
                // in that case we shouldn't do file system operation two times
                return;
            }

            throw;
        }
    }
}