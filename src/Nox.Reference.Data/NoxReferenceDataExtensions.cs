using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Abstractions;
using Nox.Reference.Abstractions.MacAddresses;
using Nox.Reference.Data.Repositories;
using Nox.Reference.Data.Seeds;

namespace Nox.Reference.Data;

public static class NoxReferenceDataExtensions
{
    public static IServiceCollection AddNoxReferenceData(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddDbContext<NoxReferenceDbContext>(options =>
        {
            options.UseSqlite("Data Source=noxreferences.db;Version=3;");
        });

        services.AddScoped<INoxReferenceSeed<IMacAddressInfo>, MacAddressDataSeed>();
        services.AddScoped<INoxReferenceRepository<IMacAddressInfo>, MacAddressRepository>();

        return services;
    }
}