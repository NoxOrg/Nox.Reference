
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nox.Reference.Data.Common.Helpers;

namespace Nox.Reference.Data.World;


//TODO this is a temporary workaround for supporting multithreading scenarios
//we shoud redesign all Nox.Reference,
//Hide EF from the client code, avoid Tracking , use immutable records, consider local cache, make tests simple to do and use, etc...


/// <summary>
/// Non thread safe world context
/// USe multiple instances for multi-threaded cenarios
/// </summary>
public sealed class WorldContext :  IDisposable
{
    private static IDbContextFactory<PooledWorldDbContext> _factory = null!;
    private static readonly object _factoryCreationLock = new();
    private bool _disposed = false;
    private readonly WorldDbContext _dbContext;
            
    public WorldContext(string dbConnectionString)
    {
        EnsureDbContextFactory(dbConnectionString);
        _dbContext = _factory.CreateDbContext();
    }

    public WorldContext()
    {
        var configuration = new ConfigurationBuilder()
          .AddJsonFile(ConfigurationConstants.ConfigFileName)
          .Build();

        EnsureDbContextFactory(configuration.GetConnectionString(ConfigurationConstants.WorldConnectionStringName)!);
        _dbContext = _factory.CreateDbContext();
    }

    public IQueryable<CountryHoliday> GetCountryHolidaysQuery()
    {
        return _dbContext.Holidays;
    }

    public IQueryable<Country> GetCountriesQuery()
    {
        return _dbContext.Countries;
    }

    public IQueryable<Culture> GetCulturesQuery()
    {
        return _dbContext.Cultures;
    }

    public IQueryable<Currency> GetCurrenciesQuery()
    {
        return _dbContext.Currencies;
    }

    public IQueryable<Language> GetLanguagesQuery()
    {
        return _dbContext.Languages;
    }

    public IQueryable<TaxNumberDefinition> GetTaxNumberDefinitionsQuery()
    {
        return _dbContext.TaxNumberDefinitions;
    }

    public IQueryable<TimeZone> GetTimeZonesQuery()
    {
        return _dbContext.TimeZones;
    }

    public IQueryable<VatNumberDefinition> GetVatNumberDefinitionsQuery()
    {
        return _dbContext.VatNumberDefinitions;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext?.Dispose();
            }
            _disposed = true;
        }
    }

    private static void EnsureDbContextFactory(string dbConnectionString)
    {
        if (_factory is not null) return;

        var optionsBuilder = new DbContextOptionsBuilder<PooledWorldDbContext>();

        // TODO: fix adding migrations. Currently throws an error of "empty db path". Need to find a way of fixing it.
        var connectionString = DatabasePathHelper.FixConnectionStringPathUsingAssemblyPath(dbConnectionString, typeof(WorldDbContext), nameof(World));

        optionsBuilder.UseSqlite(connectionString);

        lock (_factoryCreationLock)
        {
            if (_factory is not null) return;
            _factory = new PooledDbContextFactory<PooledWorldDbContext>(optionsBuilder.Options);
        }
    }
}