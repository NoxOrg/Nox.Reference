using Microsoft.Extensions.DependencyInjection;

namespace Nox.Reference.Data.World;

public static class World
{
    private static readonly IServiceProvider _serviceProvider;

    static World()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddWorldContext();

        _serviceProvider = serviceCollection.BuildServiceProvider();
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

    public static Services.PhoneNumbers.PhoneNumbersFacade PhoneNumbers =>
        new Services.PhoneNumbers.PhoneNumbersFacade(WorldDataContext);

    private static IWorldInfoContext WorldDataContext
        => _serviceProvider.GetRequiredService<IWorldInfoContext>();
}