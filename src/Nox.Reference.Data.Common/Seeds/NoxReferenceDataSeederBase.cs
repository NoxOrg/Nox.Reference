﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nox.Reference.Common;

namespace Nox.Reference.Data.Common.Seeds
{
    public abstract class NoxReferenceDataSeederBase<TDbContext, TSource, TEntity> : INoxReferenceDataSeeder
         where TDbContext : DbContext
         where TEntity : class, INoxReferenceEntity
         where TSource : class
    {
        protected readonly TDbContext _dbContext;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        protected readonly NoxReferenceFileStorageService _fileStorageService;

        protected NoxReferenceDataSeederBase(TDbContext dbContext,
                  IMapper mapper,
                  ILogger logger,
                  NoxReferenceFileStorageService fileStorageService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _fileStorageService = fileStorageService;
        }

        public abstract string TargetFileName { get; }
        public abstract string DataFolderPath { get; }

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
                var infos = GetDataInfos();
                _fileStorageService.SaveDataToFile(infos, DataFolderPath, TargetFileName);

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

        protected virtual void DoSpecialTreatAfterAdding(IEnumerable<TSource> sources, IEnumerable<TEntity> destinations)
        {
        }

        protected abstract IEnumerable<TSource> GetDataInfos();
    }
}