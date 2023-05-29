using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Nox.Reference.Data.World;

public static class World
{
    private static readonly IServiceProvider _serviceProvider;
    public static readonly IMapper Mapper;

    static World()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddWorldContext();

        _serviceProvider = serviceCollection.BuildServiceProvider();
        Mapper = _serviceProvider.GetRequiredService<IMapper>();
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
    public static IQueryable<TimeZone> TimeZones
        => WorldDataContext.TimeZones;

    public static Services.PhoneNumbers.PhoneNumbers PhoneNumbers =>
        new Services.PhoneNumbers.PhoneNumbers(WorldDataContext);

    private static IWorldInfoContext WorldDataContext
        => _serviceProvider.GetRequiredService<IWorldInfoContext>();
}
