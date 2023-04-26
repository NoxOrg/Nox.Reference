using Microsoft.Extensions.DependencyInjection;

namespace Nox.Reference.Common
{
    public static class NoxReferenceCommonExtensions
    {
        public static IServiceCollection AddNoxReferenceCommon(this IServiceCollection services)
        {
            services.AddScoped<NoxReferenceFileStorageService>();

            return services;
        }
    }
}