using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data.World;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data;

internal static class DataSeederExtensions
{
    public static IServiceCollection AddSeeders(this IServiceCollection services)
    {
        // Order matters here
        services.AddScoped<INoxReferenceDataSeeder, CurrencyDataSeeder>();
        services.AddScoped<INoxReferenceDataSeeder, VatNumberDefinitionDataSeeder>();
        services.AddScoped<INoxReferenceDataSeeder, LanguageDataSeeder>();
        services.AddScoped<INoxReferenceDataSeeder, CultureDataSeeder>();
        services.AddScoped<INoxReferenceDataSeeder, HolidayDataSeeder>();
        services.AddScoped<INoxReferenceDataSeeder, CountryDataSeeder>();
        services.AddScoped<INoxReferenceDataSeeder, TimeZoneDataSeeder>();
        services.AddScoped<INoxReferenceDataSeeder, PhoneNumbersDataSeeder>();

        return services;
    }
}