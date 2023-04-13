using AutoMapper;

namespace Nox.Reference.Data.Seeds;

internal class NoxReferenceDatabaseSeed<TType, TEnity> : INoxReferenceSeed<TType>
    where TEnity : class, INoxReferenceEntity
    where TType : class
{
    private readonly NoxReferenceDbContext _dbContext;
    private readonly IMapper _mapper;

    public NoxReferenceDatabaseSeed(
        NoxReferenceDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Seed(IEnumerable<TType> data)
    {
        var dataSet = _dbContext.Set<TEnity>();

        if (dataSet.Any())
        {
            return;
        }

        var entites = _mapper.Map<IEnumerable<TEnity>>(data);
        dataSet.AddRange(entites);

        _dbContext.SaveChanges();
    }
}