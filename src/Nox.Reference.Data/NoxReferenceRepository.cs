using Nox.Reference.Entity;

namespace Nox.Reference.Data;

public class NoxReferenceRepository<TEntity> where TEntity : class, INoxReferenceEntity
{
    private readonly NoxReferenceDbContext _dataContext;

    public NoxReferenceRepository(NoxReferenceDbContext dataContext)
    {
        _dataContext = dataContext;
    }

    public TEntity? Get(string id)
    {
        return _dataContext
            .Set<TEntity>()
            .FirstOrDefault(x => x.Id == id);
    }
}