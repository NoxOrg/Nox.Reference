using Nox.Reference.Entity;

namespace Nox.Reference.Data.Seeds;

public class NoxReferenceDatabaseSeed<TEnity> : INoxReferenceSeed<TEnity>
    where TEnity : class, INoxReferenceEntity
{
    private readonly NoxReferenceDbContext _dbContext;

    public NoxReferenceDatabaseSeed(NoxReferenceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed(IEnumerable<TEnity> data)
    {
        _dbContext.Set<TEnity>().AddRange(data);
    }
}