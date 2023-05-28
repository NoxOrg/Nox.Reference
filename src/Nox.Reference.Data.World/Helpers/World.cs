using Microsoft.Extensions.DependencyInjection;

namespace Nox.Reference.Data.World;

public static class World
{
    internal static IWorldInfoContext Create()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddWorldContext();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        return serviceProvider.GetRequiredService<IWorldInfoContext>();
    }

    public static IQueryable<Currency> Currencies
        => Create().Currencies;

    public static IQueryable<VatNumberDefinition> VatNumberDefinitions
         => Create().VatNumberDefinitions;

    public static IQueryable<Language> Languages
         => Create().Languages;

    public static IQueryable<CountryHoliday> Holidays
         => Create().Holidays;

    public static IQueryable<Culture> Cultures
        => Create().Cultures;

    public static IQueryable<TimeZone> TimeZones
        => Create().TimeZones;

    public static IQueryable<Country> Countries
        => Create().Countries;

    public static Services.PhoneNumbers.PhoneNumbers PhoneNumbers =>
        new Services.PhoneNumbers.PhoneNumbers(Create());

}
