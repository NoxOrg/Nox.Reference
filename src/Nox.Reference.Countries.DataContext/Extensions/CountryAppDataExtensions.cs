using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Country.DataContext;

public static class NoxReferenceDataExtensions
{
    public static IServiceCollection AddCountryDbContext(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsetings.json")
            .Build();

        services.AddScoped(_ => configuration);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        var connectionString = configuration.GetConnectionString(ConfigurationConstants.ConnectionStringName);
        services.AddSqlite<CountryDbContext>(connectionString);

        services.AddScoped<INoxReferenceDataSeeder, CurrencyDataSeeder>();
        services.AddScoped<ICountryContext, CountryDbContext>();
        return services;
    }
}