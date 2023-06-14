using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data;
using Nox.Reference.Data.World;

namespace Nox.Reference;

public static class WorldDataExtensions
{
    /// <summary>
    /// This method setups world context dependencies
    /// </summary>
    /// <param name="services">Current service collection</param>
    /// <returns>Modified service collection</returns>
    public static IServiceCollection AddWorldContext(this IServiceCollection services)
    {
        services.AddNoxReferenceCommon();

        services.AddDbContext<WorldDbContext>();

        services.AddSeeders();
        services.AddScoped<IWorldInfoContext, WorldDbContext>();

        services.AddScoped<IPhoneNumberService, PhoneNumberService>();

        return services;
    }
}