using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Abstractions.Currencies;
using Nox.Reference.Common;
using Nox.Reference.Data.Extensions;

namespace Nox.Reference.Currencies;

public static class AppCurrenciesExtensions
{
    public static IServiceCollection AddNoxCurrencies(this IServiceCollection services,
        IConfiguration configuartion)
    {
        services.AddScoped<ICurrenciesService, CurrenciesService>();
        services.AddNoxReferenceData(configuartion);

        return services;
    }
}