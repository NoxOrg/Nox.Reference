using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Languages.Models;
using Nox.Reference.Languages.Services;

namespace Nox.Reference.Languages;

public static class AppLanguagesExtensions
{
    private const string ResourceName = "Nox.Reference.Languages.json";
    private static readonly object _syncObj = new();
    private static bool _initialized = false;

    public static IServiceCollection AddNoxLanguages(this IServiceCollection services)
    {
        InitLanguages();

        services.AddScoped<ILanguagesService, LanguagesService>();

        return services;
    }

    private static void InitLanguages()
    {
        lock (_syncObj)
        {
            if (_initialized)
            {
                return;
            }

            var languages = AssemblyDataInitializer.GetDataFromAssemblyResource<LanguageInfo>(ResourceName);
            LanguagesService.Init(languages);

            _initialized = true;
        }
    }
}