using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.MacAddresses.DataContext;

namespace Nox.Reference.MacAddresses;

public static class AppMacAddressExtensions
{
    public static IServiceCollection AddNoxMacAddresses(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlite<MacAddressDbContext>(configuration.GetConnectionString(ConfigurationConstants.ConnectionStringName));
        return services;
    }
}