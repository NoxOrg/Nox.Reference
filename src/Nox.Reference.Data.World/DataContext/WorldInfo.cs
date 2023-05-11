using System.Reflection;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nox.Reference.Abstractions;
using Nox.Reference.Abstractions.Cultures;
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
        => WorldDataContext.Currencies;

    public static IQueryable<IVatNumberDefinitionInfo> VatNumberDefinitions
        => WorldDataContext.VatNumberDefinitions;

    public static IQueryable<ILanguageInfo> Languages
        => WorldDataContext.Languages;

    public static IQueryable<ICountryHolidayInfo> Holidays
        => WorldDataContext.Holidays;

    public static IQueryable<ICultureInfo> Cultures
        => WorldDataContext.Cultures;

    public static IQueryable<ICountryInfo> Countries
        => WorldDataContext.Countries;

    public static IQueryable<INativeNameInfo> GetCountryTranslationsForLanguage(string languageCode)
    {
        return DbContext.Set<CountryNameTranslation>()
            .Where(x => x.Language.Iso_639_1 == languageCode)
            .AsNoTracking()
            .ProjectTo<NativeNameInfo>(_mapper.ConfigurationProvider);
    }

    public static IQueryable<ICountryInfo> GetCountriesThatUseLanguage(string languageCode)
    {
        return DbContext.Set<Language>()
            .Where(x => x.Iso_639_1 == languageCode)
            .SelectMany(x => x.Countries)
            .AsNoTracking()
            .ProjectTo<CountryInfo>(_mapper.ConfigurationProvider);
    }

    private static IWorldInfoContext WorldDataContext
        => DbContext;

    private static WorldDbContext DbContext
        => new WorldDbContext(new DbContextOptions<WorldDbContext>(), _mapper, _configuration);
}