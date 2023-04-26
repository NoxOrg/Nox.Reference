using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nox.Reference.Common;

public static class ConfigurationExtensions
{
    private static readonly ConfigurationBuilder _configBuilder = new ConfigurationBuilder();

    public static IServiceCollection AddNoxReferenceConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        _configBuilder.AddConfiguration(configuration);
        services.AddScoped<IConfiguration>(_ => _configBuilder.Build());

        return services;
    }
}