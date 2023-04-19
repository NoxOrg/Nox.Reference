using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nox.Reference.Abstractions.Currencies;
using Nox.Reference.Common;
using Nox.Reference.Country.DataContext.Configurations;

namespace Nox.Reference.Country.DataContext;

internal class CountryDbContext : DbContext, ICountryContext
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    // DON'T remove default constructor. It is used for migrations purposes.
    public CountryDbContext()
    {
    }

    public CountryDbContext(
        DbContextOptions<CountryDbContext> options,
        IMapper mapper,
        IConfiguration configuration)
        : base(options)
    {
        _mapper = mapper;
        _configuration = configuration;
    }

    public IQueryable<ICurrencyInfo> Currencies
      => Set<Currency>()
         .AsQueryable()
         .ProjectTo<ICurrencyInfo>(_mapper.ConfigurationProvider);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var connectionString = _configuration.GetConnectionString(ConfigurationConstants.ConnectionStringName);
        optionsBuilder.UseSqlite(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //IMPORTANT: Only the following line should be used to add configurations when all structure will be done.
        //var configurations = Assembly.GetExecutingAssembly();

        //modelBuilder.ApplyConfigurationsFromAssembly(configurations);
        //modelBuilder.ApplyConfiguration(new CountryConfiguration());
        //modelBuilder.ApplyConfiguration(new CountryLocalizationConfiguration());
        //modelBuilder.ApplyConfiguration(new LanguageConfiguration());
        //modelBuilder.ApplyConfiguration(new TopLevelDomainConfiguration());
        //modelBuilder.ApplyConfiguration(new TopLevelDomainLocalizationConfiguration());
        //modelBuilder.ApplyConfiguration(new CityConfiguration());

        //modelBuilder.ApplyConfiguration(new FlagsConfiguration());
        //modelBuilder.ApplyConfiguration(new GiniCoefficientConfiguration());
        //modelBuilder.ApplyConfiguration(new TimeZoneInfoConfiguration());

        //modelBuilder.ApplyConfiguration(new HolidayDataConfiguration());

        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyUsageConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyFrequentUsageConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyRareUsageConfiguration());

        modelBuilder.ApplyConfiguration(new MinorCurrencyUnitConfiguration());
        modelBuilder.ApplyConfiguration(new MajorCurrencyUnitConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}