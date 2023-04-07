using AutoMapper;
using Nox.Reference.Abstractions;
using Nox.Reference.Data.Entities;

namespace Nox.Reference.Data.Seeds;

public abstract class NoxReferenceDatabaseSeedBase<TType, TEnity> : INoxReferenceSeed<TType>
    where TEnity : class, INoxReferenceEntity
    where TType : class
{
    private readonly NoxReferenceDbContext _dbContext;
    private readonly IMapper _mapper;

    protected NoxReferenceDatabaseSeedBase(
        NoxReferenceDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Seed(IEnumerable<TType> data)
    {
        var entites = _mapper.Map<TEnity>(data);

        _dbContext.Set<TEnity>().AddRangeAsync(entites);

        _dbContext.SaveChanges();
    }
}