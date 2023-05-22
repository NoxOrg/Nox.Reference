using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nox.Reference.Common;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World;

internal class WorldDbContext : DbContext, IWorldInfoContext
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        var connectionString = _configuration.GetConnectionString(ConfigurationConstants.WorldConnectionStringName);
        optionsBuilder.UseSqlite(connectionString, opts =>
        {
            opts.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        });//.LogTo(Console.WriteLine);// -- Use the following method for debug purposes
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var configurations = Assembly.GetExecutingAssembly();
        modelBuilder.ApplyConfigurationsFromAssembly(configurations);

        base.OnModelCreating(modelBuilder);
    }

    private IQueryable<TSource> GetData<TSource>()
        where TSource : class, INoxReferenceEntity
        => Set<TSource>()
            .IncludeAll()
            .AsNoTracking();
}