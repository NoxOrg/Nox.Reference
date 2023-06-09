﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nox.Reference.Common;

namespace Nox.Reference.Data.Common.Seeds;

/// <summary>
/// This base class is used to seed data from different sources
/// into .json files of Nox.Reference format
/// </summary>
/// <typeparam name="TDbContext">Context to be populated</typeparam>
/// <typeparam name="TSource">Flat model</typeparam>
/// <typeparam name="TEntity">Entity framework model</typeparam>
internal abstract class NoxReferenceDataSeederBase<TDbContext, TSource, TEntity> : INoxReferenceDataSeeder
     where TDbContext : DbContext
     where TEntity : NoxReferenceEntityBase
     where TSource : class
{
    protected readonly TDbContext _dbContext;
    protected readonly IMapper _mapper;
    protected readonly ILogger _logger;
    protected readonly NoxReferenceFileStorageService _fileStorageService;
    private readonly bool _importDataToJson;

    protected NoxReferenceDataSeederBase(
        TDbContext dbContext,
        IMapper mapper,
        ILogger logger,
        NoxReferenceFileStorageService fileStorageService,
        bool importDataToJson = true)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
        _fileStorageService = fileStorageService;
        _importDataToJson = importDataToJson;
    }

    /// <summary>
    /// Defines files which imported data will be saved.
    /// </summary>
    public abstract string TargetFileName { get; }

    /// <summary>
    /// Defines folder where imported data will be saved.
    /// </summary>
    public abstract string DataFolderPath { get; }

    /// <summary>
    /// Carries out seeding ta flow.
    /// Import data from external source.
    /// Transform daa from dto to particular entity.
    /// Save imported data to aoutput files.
    /// </summary>
    /// <exception cref="NoxDataExtractorException"></exception>
    public void Seed()
    {
        var dataSet = _dbContext
            .Set<TEntity>();

        if (dataSet.Any())
        {
            _logger.LogInformation("Data set {dataSet} already contains data.", typeof(TEntity).Name);
            return;
        }

        _logger.LogInformation("Start seeding {dataSet}...", typeof(TEntity).Name);
        try
        {
            var infos = GetFlatEntitiesFromDataSources();
            if (_importDataToJson)
            {
                _fileStorageService.SaveDataToFile(infos, DataFolderPath, TargetFileName);
            }
            var entities = _mapper.Map<IEnumerable<TEntity>>(infos);

            dataSet.AddRange(entities);
            _dbContext.SaveChanges();

            DoSpecialTreatAfterAdding(infos, entities);

            _logger.LogInformation("End seeding {dataSet}...", typeof(TEntity).Name);
        }
        catch (Exception ex)
        {
            var errorMessage = $"Error occurs during seeding {typeof(TEntity).Name}.Error: {ex.Message}";
            _logger.LogError(errorMessage);

            throw new NoxDataExtractorException(errorMessage);
        }
    }

    /// <summary>
    /// Any special action that needs to be done after seeding
    /// For example fixing navigation references to set them properly between entities
    /// </summary>
    /// <param name="sources">Flat models collection</param>
    /// <param name="destinations">Entity framework model collection</param>
    protected virtual void DoSpecialTreatAfterAdding(IEnumerable<TSource> sources, IEnumerable<TEntity> destinations)
    { }

    /// <summary>
    /// Method to fetch and format data from different sources into
    /// flat models
    /// </summary>
    /// <returns>A collection of fetched data formatted into flat models</returns>
    protected abstract IReadOnlyList<TSource> GetFlatEntitiesFromDataSources();
}