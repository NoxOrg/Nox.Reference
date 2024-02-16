using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data.World;

namespace Nox.Reference;

/// <summary>
/// Non thread safe world context
/// Create multiple instance of this class if you need to use it in a multi-threaded environment
/// </summary>
[Obsolete("Use WorldContext instead")]
public static class World
{
    private static readonly IServiceProvider _serviceProvider;

    static World()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddWorldContext();

        _serviceProvider = serviceCollection.BuildServiceProvider();

        Mapper = _serviceProvider.GetRequiredService<IMapper>();
    }

    internal static IMapper Mapper { get; }

    public static IQueryable<Currency> Currencies
        => WorldDataContext.Currencies;

    public static IQueryable<VatNumberDefinition> VatNumberDefinitions
        => WorldDataContext.VatNumberDefinitions;

    public static IQueryable<TaxNumberDefinition> TaxNumberDefinitions
        => WorldDataContext.TaxNumberDefinitions;

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

    public static PhoneNumbersFacade PhoneNumbers =>
        new PhoneNumbersFacade(WorldDataContext);

    private static IWorldInfoContext WorldDataContext
        => _serviceProvider.GetRequiredService<IWorldInfoContext>();    
}