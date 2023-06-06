using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nox.Reference.Common;

public static class NoxReferenceCommonExtensions
{
    private static readonly IConfiguration _configuration = new ConfigurationBuilder()
              .AddJsonFile(ConfigurationConstants.ConfigFileName)
              .Build();

    public static IServiceCollection AddNoxReferenceCommon(this IServiceCollection services)
    {
        services.AddScoped<NoxReferenceFileStorageService>();
        services.AddAutoMapper(Assembly.GetCallingAssembly());

        services.AddSingleton(_configuration);
        return services;
    }

    public static IConfiguration GetNoxReferenceConfiguration(this IServiceCollection _)
    {
        return _configuration;
    }
}