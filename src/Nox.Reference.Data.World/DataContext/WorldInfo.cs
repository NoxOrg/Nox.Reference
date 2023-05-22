using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

    public static IQueryable<Currency> Currencies
        => WorldDataContext.Currencies;

    public static IQueryable<VatNumberDefinition> VatNumberDefinitions
        => WorldDataContext.VatNumberDefinitions;

    public static IQueryable<Language> Languages
        => WorldDataContext.Languages;

    public static IQueryable<CountryHoliday> Holidays
        => WorldDataContext.Holidays;

    public static IQueryable<Culture> Cultures
        => WorldDataContext.Cultures;

    public static IQueryable<Country> Countries
        => WorldDataContext.Countries;

    private static IWorldInfoContext WorldDataContext
        => DbContext;

    private static WorldDbContext DbContext
        => new WorldDbContext(new DbContextOptions<WorldDbContext>(), _mapper, _configuration);
}