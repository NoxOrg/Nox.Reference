using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Nox.Reference.Data.Configurations;

namespace Nox.Reference.Data;

public interface INoxReferenceContext
{
    IQueryable<TSet> GetSet<TSet>();
}

internal class NoxReferenceDbContext : DbContext
{
    public NoxReferenceDbContext(DbContextOptions<NoxReferenceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        modelBuilder.ApplyConfiguration(new MacAddressConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}