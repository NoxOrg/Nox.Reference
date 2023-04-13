using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Abstractions.Currencies;
using Nox.Reference.Common;

namespace Nox.Reference.Currencies;

public static class AppCurrenciesExtensions
{
    public static IServiceCollection AddNoxCurrencies(this IServiceCollection services)
    {
        services.AddScoped<ICurrenciesService, CurrenciesService>();

        return services;
    }
}