using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Nox.Reference.Data.Repositories;

internal class NoxReferenceContext<TEntity, TType> : INoxReferenceContext<TType>
    where TType : class
    where TEntity : class, INoxReferenceEntity
{
    private readonly NoxReferenceDbContext _dataContext;
    private readonly IMapper _mapper;

    public NoxReferenceContext(
        NoxReferenceDbContext dataContext,
        IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public IQueryable<TType> Query
        => _dataContext
            .Set<TEntity>()
            .AsQueryable()
            .ProjectTo<TType>(_mapper.ConfigurationProvider);
}