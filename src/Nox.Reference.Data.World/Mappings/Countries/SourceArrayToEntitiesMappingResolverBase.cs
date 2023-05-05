using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Nox.Reference.Data.Common;

namespace Nox.Reference.Data.World.Mappings;

internal abstract class SourceArrayToEntitiesMappingResolverBase<TSource, TEntity> : ITypeConverter<IReadOnlyList<TSource>, IReadOnlyList<TEntity>>
    where TEntity : class, INoxReferenceEntity
{
    protected readonly WorldDbContext _worldDbContext;
    private readonly IMapper _mapper;
    protected readonly ILogger _logger;

    protected SourceArrayToEntitiesMappingResolverBase(
        WorldDbContext worldDbContext,
        IMapper mapper,
        ILogger logger)
    {
        _worldDbContext = worldDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    protected abstract Expression<Func<TEntity, bool>> KeySearchExpression(TSource source);

    public IReadOnlyList<TEntity> Convert(IReadOnlyList<TSource> source, IReadOnlyList<TEntity> destination, ResolutionContext context)
    {
        var items = new List<TEntity>();
        var dbSet = _worldDbContext
            .Set<TEntity>();

        foreach (var sourceItem in source)
        {
            try
            {
                var exp = KeySearchExpression(sourceItem);
                var entity = dbSet.FirstOrDefault(exp);

                if (entity == null)
                {
                    entity = _mapper.Map<TEntity>(sourceItem);
                    dbSet.Add(entity);
                }
                items.Add(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurs during mapping {source}. Error:{error}", sourceItem, ex.Message);
            }
        }

        _worldDbContext.SaveChanges();

        destination = items;

        return destination;
    }
}