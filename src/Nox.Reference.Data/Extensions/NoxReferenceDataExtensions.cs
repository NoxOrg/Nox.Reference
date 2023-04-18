using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nox.Reference.Country.DataContext;

public static class NoxReferenceDataExtensions
{
    public static IServiceCollection AddNoxReferenceData(this IServiceCollection services, IConfiguration configuration)
    {
        //    services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //    services.AddScoped<INoxReferenceSeed<IMacAddressInfo>, NoxReferenceDatabaseSeed<IMacAddressInfo, MacAddress>>();
        //    services.AddScoped<INoxReferenceSeed<ICurrencyInfo>, NoxReferenceDatabaseSeed<ICurrencyInfo, Currency>>();

        //    services.AddScoped<INoxReferenceContext<IMacAddressInfo>, NoxReferenceContext<MacAddress, IMacAddressInfo>>();

        //    services.AddScoped<INoxReferenceContext<ICurrencyInfo>, NoxReferenceContext<Currency, ICurrencyInfo>>();
        //    services.AddScoped<INoxReferenceContext<ICurrencyUsage>, NoxReferenceContext<CurrencyUsage, ICurrencyUsage>>();

        //    services.AddScoped<INoxReferenceDatabaseMigrator, DatabaseMigrator>();
        //    services.AddSqlite<NoxReferenceDbContext>(configuration.GetConnectionString("noxreferencesConnection"));
        return services;
    }
}