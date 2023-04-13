using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Abstractions.Currencies;
using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.Data.Repositories;
using Nox.Reference.Data.Seeds;

namespace Nox.Reference.Data.Extensions;

public static class NoxReferenceDataExtensions
{
    public static IServiceCollection AddNoxReferenceData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<INoxReferenceSeed<IMacAddressInfo>, NoxReferenceDatabaseSeed<IMacAddressInfo, MacAddress>>();
        services.AddScoped<INoxReferenceSeed<ICurrencyInfo>, NoxReferenceDatabaseSeed<ICurrencyInfo, Currency>>();

        services.AddScoped<INoxReferenceContext<IMacAddressInfo>, NoxReferenceContext<MacAddress, IMacAddressInfo>>();

        services.AddScoped<INoxReferenceDatabaseMigrator, DatabaseMigrator>();
        services.AddSqlite<NoxReferenceDbContext>(configuration.GetConnectionString("noxreferencesConnection"));
        return services;
    }
}