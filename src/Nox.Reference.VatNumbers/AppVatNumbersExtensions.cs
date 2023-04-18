using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Holidays;
using Nox.Reference.VatNumbers.Models;

namespace Nox.Reference.VatNumbers;

public static class AppVatNumbersExtensions
{
    private const string ResourceName = "Nox.Reference.VatNumbers.json";
    private static readonly object _syncObj = new();
    private static bool _initialized = false;

    public static IServiceCollection AddNoxVatNumbers(this IServiceCollection services)
    {
        InitVatNumberService();

        services.AddScoped<IVatNumberService, VatNumberService>();

        return services;
    }

    private static void InitVatNumberService()
    {
        lock (_syncObj)
        {
            if (_initialized)
            {
                return;
            }

            //var vatNumbers = AssemblyDataInitializer.GetDataFromAssemblyResource<VatNumberInfo>(ResourceName);
            //VatNumberService.Init(vatNumbers);

            _initialized = true;
        }
    }
}