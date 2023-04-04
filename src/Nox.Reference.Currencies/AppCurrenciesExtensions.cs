using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Abstractions.Currencies;
using Nox.Reference.Common;

namespace Nox.Reference.Currencies;

public static class AppCurrenciesExtensions
{
    private const string ResourceName = "Nox.Reference.Currencies.json";
    private static readonly object _syncObj = new();
    private static bool _initialized = false;

    public static IServiceCollection AddNoxCurrencies(this IServiceCollection services)
    {
        InitMacAddressService();

        services.AddScoped<ICurrenciesService, CurrenciesService>();

        return services;
    }

    private static void InitMacAddressService()
    {
        lock (_syncObj)
        {
            if (_initialized)
            {
                return;
            }

            var currencies = AssemblyDataInitializer.GetDataFromAssemblyResource<CurrencyInfo>(ResourceName);
            CurrenciesService.Init(currencies);

            _initialized = true;
        }
    }
}