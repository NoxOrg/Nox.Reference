using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nox.Reference.Data.Common.Helpers;
using System.Reflection;

namespace Nox.Reference;

// TODO: possibly cache this and machine context itself
public class WorldDbContext : DbContext, IWorldInfoContext
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private static string? _databasePath;

    // DON'T remove default constructor. It is used for migrations purposes.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public WorldDbContext()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public WorldDbContext(
        DbContextOptions<WorldDbContext> options,
        IMapper mapper,
        IConfiguration configuration)
        : base(options)
    {
        _mapper = mapper;
        _configuration = configuration;
    }

    public IQueryable<Currency> Currencies
        => GetData<Currency>();

    public IQueryable<VatNumberDefinition> VatNumberDefinitions
         => GetData<VatNumberDefinition>();

    public IQueryable<TaxNumberDefinition> TaxNumberDefinitions
         => GetData<TaxNumberDefinition>();

    public IQueryable<Language> Languages
         => GetData<Language>();

    public IQueryable<CountryHoliday> Holidays
         => GetData<CountryHoliday>();

    public IQueryable<Culture> Cultures
        => GetData<Culture>();

    public IQueryable<TimeZone> TimeZones
        => GetData<TimeZone>();

    public IQueryable<Country> Countries
        => GetData<Country>();

    public IQueryable<CarrierPhoneNumber> CarrierPhoneNumbers
        => GetData<CarrierPhoneNumber>();

    public IQueryable<PhoneCarrier> PhoneCarriers
        => GetData<PhoneCarrier>();

    /// <summary>
    /// <para>Override default database path. Examples: </para>
    /// <para>'Data Source=.\NoxReferenceDatabase\Nox.Reference.World.db'</para>
    /// <para>'Data Source=..\..\data\Nox.Reference.World.db'</para>
    /// <para>'Data Source=C:\project\NoxReferenceDatabase\Nox.Reference.World.db'</para>
    /// </summary>
    /// <param name="databasePath">New overridden database connection string</param>
    public static void UseDatabaseConnectionString(string databasePath)
    {
        _databasePath = databasePath;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var connectionString = _databasePath ?? _configuration.GetConnectionString(ConfigurationConstants.WorldConnectionStringName);

        // TODO: fix adding migrations. Currently throws an error of "empty db path". Need to find a way of fixing it.
        //connectionString = DatabasePathHelper.FixConnectionStringPathUsingAssemblyPath(connectionString, typeof(WorldDbContext), nameof(World));

        optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlite(connectionString);//.LogTo(Console.WriteLine);// -- Use the following method for debug purposes
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var configurations = Assembly.GetExecutingAssembly();
        modelBuilder.ApplyConfigurationsFromAssembly(configurations);

        base.OnModelCreating(modelBuilder);
    }

    private IQueryable<TSource> GetData<TSource>()
        where TSource : NoxReferenceEntityBase
        => Set<TSource>();
}