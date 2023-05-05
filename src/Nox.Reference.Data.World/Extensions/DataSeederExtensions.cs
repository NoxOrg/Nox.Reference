using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data.World;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data;

internal static class DataSeederExtensions
{
    public static IServiceCollection AddSeeders(this IServiceCollection services)
    {
        services.AddScoped<INoxReferenceDataSeeder, CurrencyDataSeeder>();
        services.AddScoped<INoxReferenceDataSeeder, VatNumberDefinitionDataSeeder>();
        services.AddScoped<INoxReferenceDataSeeder, LanguageDataSeeder>();
        services.AddScoped<INoxReferenceDataSeeder, HolidayDataSeeder>();
        services.AddScoped<INoxReferenceDataSeeder, CultureDataSeeder>();
        services.AddScoped<INoxReferenceDataSeeder, TimeZoneDataSeeder>();

        return services;
    }
}