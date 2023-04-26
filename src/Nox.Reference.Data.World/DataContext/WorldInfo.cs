using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nox.Reference.Abstractions;
using Nox.Reference.Abstractions.Cultures;
using Nox.Reference.Common;
using Nox.Reference.Data.World;

namespace Nox.Reference.Data;

public static class WorldInfo
{
    private static readonly IWorldInfoContext _dbContext;

#pragma warning disable S3963 // "static" fields should be initialized inline

    static WorldInfo()
#pragma warning restore S3963 // "static" fields should be initialized inline
    {
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(Assembly.GetExecutingAssembly());
        });
        var mapper = mapperConfiguration.CreateMapper();
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile(ConfigurationConstants.WorldConfigFileName)
            .Build();
        _dbContext = new WorldDbContext(new DbContextOptions<WorldDbContext>(), mapper, configuration);
    }

    public static IQueryable<ICurrencyInfo> Currencies
        => _dbContext.Currencies;

    public static IQueryable<ICultureInfo> Cultures
        => _dbContext.Cultures;
}