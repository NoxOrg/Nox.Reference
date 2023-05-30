using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Common;
using Nox.Reference.Data.World;
using Nox.Reference.PhoneNumbers;

namespace Nox.Reference.Data;

public static class WorldDataExtensions
{
    public static IServiceCollection AddWorldContext(this IServiceCollection services)
    {
        services.AddNoxReferenceCommon();

        services.AddDbContext<WorldDbContext>();

        services.AddSeeders();
        services.AddScoped<IWorldInfoContext, WorldDbContext>();

        services.AddSingleton<IPhoneNumberService, PhoneNumberService>();

        return services;
    }
}