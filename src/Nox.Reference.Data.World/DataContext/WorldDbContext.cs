﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nox.Reference.Abstractions;
using Nox.Reference.Common;
using Nox.Reference.Data.World;
using Nox.Reference.Data.World.Configurations;

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

    public IQueryable<ICurrencyInfo> Currencies
        => Set<Currency>()
             .AsNoTracking()
             .AsQueryable()
             .ProjectTo<CurrencyInfo>(_mapper.ConfigurationProvider);

    public IQueryable<IVatNumberDefinitionInfo> VatNumberDefinitions
         => Set<VatNumberDefinition>()
            .AsNoTracking()
            .AsQueryable()
            .ProjectTo<VatNumberDefinitionInfo>(_mapper.ConfigurationProvider);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var connectionString = _configuration.GetConnectionString(ConfigurationConstants.WorldConnectionStringName);
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

        modelBuilder.ApplyConfiguration(new VatNumberDefinitionConfiguration());
        modelBuilder.ApplyConfiguration(new VatNumberValidationRuleConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}