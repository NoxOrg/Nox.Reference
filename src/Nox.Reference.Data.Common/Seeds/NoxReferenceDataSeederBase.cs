using AutoMapper;
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
        private readonly TDbContext _dbContext;
        private readonly IMapper _mapper;
        protected readonly ILogger _logger;

        protected NoxReferenceDataSeederBase(TDbContext dbContext,
                  IMapper mapper,
                  ILogger logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        protected abstract IEnumerable<TSource> GetDataInfos();

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

                var entities = _mapper.Map<IEnumerable<TEntity>>(infos);

                dataSet.AddRange(entities);

                _dbContext.SaveChanges();

                _logger.LogInformation("Start seeding {dataSet}...", typeof(TEntity).Name);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error occurs during seeding {typeof(TEntity).Name}.Error: {ex.Message}";
                _logger.LogError(errorMessage);

                throw new NoxDataExtractorException(errorMessage);
            }
        }
    }
}