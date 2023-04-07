using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data;

namespace Nox.Reference.Common;

public static class NoxReferenceAppServiceExtensions
{
    public static IServiceCollection AddNoxReferenceCommon(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddDbContext<NoxReferenceDbContext>(options =>
        {
            var connectionString = "TODO://Executing assembly path";
            options.UseSqlite(connectionString);
        });

        return services;
    }
}