using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nox.Reference.Abstractions;
using Nox.Reference.Common;
using Nox.Reference.Data.World;

namespace Nox.Reference.Data;

public static class WorldInfo
{
    private static readonly IConfiguration _configuration;
    private static readonly IMapper _mapper;

#pragma warning disable S3963 // "static" fields should be initialized inline

    static WorldInfo()
#pragma warning restore S3963 // "static" fields should be initialized inline
    {
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(Assembly.GetExecutingAssembly());
        });
        _mapper = mapperConfiguration.CreateMapper();
        _configuration = new ConfigurationBuilder()
            .AddJsonFile(ConfigurationConstants.ConfigFileName)
            .Build();
    }

    public static IQueryable<ICurrencyInfo> Currencies
        => DbContext.Currencies;

    public static IQueryable<IVatNumberDefinitionInfo> VatNumberDefinitions
        => DbContext.VatNumberDefinitions;

    public static IQueryable<ILanguageInfo> Languages
        => DbContext.Languages;

    private static IWorldInfoContext DbContext
        => new WorldDbContext(new DbContextOptions<WorldDbContext>(), _mapper, _configuration);
}