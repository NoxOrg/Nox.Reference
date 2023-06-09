﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Nox.Reference.Data.World;

namespace Nox.Reference;

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

    /// <summary>
    /// <para>Override default database path. Examples: </para>
    /// <para>'Data Source=.\NoxReferenceDatabase\Nox.Reference.World.db'</para>
    /// <para>'Data Source=..\..\data\Nox.Reference.World.db'</para>
    /// <para>'Data Source=C:\project\NoxReferenceDatabase\Nox.Reference.World.db'</para>
    /// </summary>
    /// <param name="path">New overridden database connection string</param>
    public static void UseDatabaseConnectionString(string path)
        => WorldDbContext.UseDatabaseConnectionString(path);
}