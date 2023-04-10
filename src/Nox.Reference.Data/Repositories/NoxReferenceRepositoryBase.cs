using AutoMapper;

namespace Nox.Reference.Data.Repositories;

public abstract class NoxReferenceRepositoryBase<TEntity, TType> : INoxReferenceRepository<TType>
    where TType : class
    where TEntity : class, INoxReferenceEntity
{
    private readonly NoxReferenceDbContext _dataContext;
    private readonly IMapper _mapper;

    protected NoxReferenceRepositoryBase(
        NoxReferenceDbContext dataContext,
        IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public TType? Get(int id)
    {
        var entity = _dataContext
            .Set<TEntity>()
            .FirstOrDefault(x => x.Id == id);

        if (entity == null)
        {
            return null;
        }

        var info = _mapper.Map<TType>(entity);

        return info;
    }
}