using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.MacAddresses.DataContext;

namespace Nox.Reference.Data.Extensions;

public static class MacAddressDataExtensions
{
    public static IServiceCollection AddMacAddressDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddSqlite<MacAddressDbContext>(
            configuration.GetConnectionString(ConfigurationConstants.ConnectionStringName)
        );
        return services;
    }
}